// Assets/Editor/ReferenceFinderWindow.cs
// Finds serialized references to a target GameObject (or its Transform/Components)
// across open scenes and (optionally) prefabs / scriptable objects in the project.

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class ReferenceFinderWindow : EditorWindow
{
    [Serializable]
    private sealed class Hit
    {
        public UnityEngine.Object owner;   // Component or ScriptableObject/etc that holds the reference
        public string ownerContext;        // Scene name or asset path
        public string propertyPath;        // SerializedProperty path
        public UnityEngine.Object referencedValue; // What exactly was referenced (GO/Transform/Component)
    }

    private UnityEngine.Object _target;
    private bool _searchOpenScenes = true;
    private bool _searchPrefabs = true;
    private bool _searchScriptableObjects = true;

    private readonly List<Hit> _hits = new();
    private Vector2 _scroll;

    [MenuItem("Tools/Reference Finder")]
    public static void Open()
    {
        GetWindow<ReferenceFinderWindow>("Reference Finder");
    }

    private void OnGUI()
    {
        EditorGUILayout.Space(6);

        using (new EditorGUILayout.VerticalScope("box"))
        {
            EditorGUILayout.LabelField("Target", EditorStyles.boldLabel);
            _target = EditorGUILayout.ObjectField(_target, typeof(UnityEngine.Object), true);

            EditorGUILayout.Space(6);
            EditorGUILayout.LabelField("Where to search", EditorStyles.boldLabel);
            _searchOpenScenes = EditorGUILayout.ToggleLeft("Open Scenes", _searchOpenScenes);
            _searchPrefabs = EditorGUILayout.ToggleLeft("Project Prefabs", _searchPrefabs);
            _searchScriptableObjects = EditorGUILayout.ToggleLeft("Project ScriptableObjects", _searchScriptableObjects);

            EditorGUILayout.Space(8);

            using (new EditorGUI.DisabledScope(_target == null))
            {
                if (GUILayout.Button("Find References", GUILayout.Height(28)))
                {
                    FindReferences();
                }
            }
        }

        EditorGUILayout.Space(8);

        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField($"Results: {_hits.Count}", EditorStyles.boldLabel);
            if (_hits.Count > 0 && GUILayout.Button("Clear", GUILayout.Width(80)))
                _hits.Clear();
        }

        EditorGUILayout.Space(4);

        _scroll = EditorGUILayout.BeginScrollView(_scroll);
        foreach (var h in _hits)
        {
            using (new EditorGUILayout.VerticalScope("box"))
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    // Owner
                    EditorGUILayout.ObjectField(h.owner, typeof(UnityEngine.Object), true);

                    if (GUILayout.Button("Select", GUILayout.Width(70)))
                    {
                        Selection.activeObject = h.owner;
                        EditorGUIUtility.PingObject(h.owner);
                    }
                }

                EditorGUILayout.LabelField("Context", h.ownerContext);
                EditorGUILayout.LabelField("Property", h.propertyPath);

                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField("Value", GUILayout.Width(40));
                    EditorGUILayout.ObjectField(h.referencedValue, typeof(UnityEngine.Object), true);
                }
            }
        }
        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space(6);
        EditorGUILayout.HelpBox(
            "This finds *serialized* references (Inspector fields). " +
            "It won't detect runtime lookups like FindObjectOfType/GameObject.Find, string-based links, tags, Addressables runtime loads, etc.",
            MessageType.Info
        );
    }

    private void FindReferences()
    {
        _hits.Clear();

        if (_target == null)
            return;

        var targets = BuildTargetSet(_target);
        if (targets.Count == 0)
            return;

        try
        {
            if (_searchOpenScenes)
                ScanOpenScenes(targets);

            if (_searchPrefabs)
                ScanPrefabs(targets);

            if (_searchScriptableObjects)
                ScanScriptableObjects(targets);
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }

        // Sort for nicer reading
        _hits.Sort((a, b) =>
        {
            int c = string.Compare(a.ownerContext, b.ownerContext, StringComparison.OrdinalIgnoreCase);
            if (c != 0) return c;
            c = string.Compare(a.owner.name, b.owner.name, StringComparison.OrdinalIgnoreCase);
            if (c != 0) return c;
            return string.Compare(a.propertyPath, b.propertyPath, StringComparison.OrdinalIgnoreCase);
        });
    }

    private static HashSet<UnityEngine.Object> BuildTargetSet(UnityEngine.Object picked)
    {
        var set = new HashSet<UnityEngine.Object>();
        if (picked == null) return set;

        set.Add(picked);

        if (picked is GameObject go)
        {
            set.Add(go.transform);
            foreach (var c in go.GetComponents<Component>())
                if (c != null) set.Add(c);
        }
        else if (picked is Component comp)
        {
            set.Add(comp.gameObject);
            set.Add(comp.transform);
            foreach (var c in comp.gameObject.GetComponents<Component>())
                if (c != null) set.Add(c);
        }

        return set;
    }

    private void ScanOpenScenes(HashSet<UnityEngine.Object> targets)
    {
        // Scan ALL open scenes (including inactive)
        int sceneCount = SceneManager.sceneCount;
        for (int i = 0; i < sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (!scene.isLoaded) continue;

            var roots = scene.GetRootGameObjects();
            foreach (var root in roots)
            {
                // Components on root + children
                var comps = root.GetComponentsInChildren<Component>(true);
                foreach (var comp in comps)
                {
                    if (comp == null) continue; // missing script
                    ScanSerializedObject(comp, $"Scene: {scene.name}", targets);
                }
            }
        }
    }

    private void ScanPrefabs(HashSet<UnityEngine.Object> targets)
    {
        var guids = AssetDatabase.FindAssets("t:Prefab");
        for (int i = 0; i < guids.Length; i++)
        {
            if (i % 20 == 0)
            {
                if (EditorUtility.DisplayCancelableProgressBar(
                        "Reference Finder", "Scanning prefabs...",
                        (float)i / Math.Max(1, guids.Length)))
                {
                    break;
                }
            }

            var path = AssetDatabase.GUIDToAssetPath(guids[i]);
            var prefabRoot = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefabRoot == null) continue;

            var comps = prefabRoot.GetComponentsInChildren<Component>(true);
            foreach (var comp in comps)
            {
                if (comp == null) continue;
                ScanSerializedObject(comp, $"Prefab: {path}", targets);
            }
        }
    }

    private void ScanScriptableObjects(HashSet<UnityEngine.Object> targets)
    {
        var guids = AssetDatabase.FindAssets("t:ScriptableObject");
        for (int i = 0; i < guids.Length; i++)
        {
            if (i % 50 == 0)
            {
                if (EditorUtility.DisplayCancelableProgressBar(
                        "Reference Finder", "Scanning ScriptableObjects...",
                        (float)i / Math.Max(1, guids.Length)))
                {
                    break;
                }
            }

            var path = AssetDatabase.GUIDToAssetPath(guids[i]);
            var so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
            if (so == null) continue;

            ScanSerializedObject(so, $"ScriptableObject: {path}", targets);
        }
    }

    private void ScanSerializedObject(UnityEngine.Object owner, string context, HashSet<UnityEngine.Object> targets)
    {
        // SerializedObject can throw on some internal Unity objects; keep it safe.
        SerializedObject so;
        try
        {
            so = new SerializedObject(owner);
        }
        catch
        {
            return;
        }

        var prop = so.GetIterator();
        bool enterChildren = true;

        while (prop.Next(enterChildren))
        {
            enterChildren = true;

            if (prop.propertyType != SerializedPropertyType.ObjectReference)
                continue;

            if (prop.name == "m_Script") // skip script pointer noise
                continue;

            var val = prop.objectReferenceValue;
            if (val == null) continue;

            if (!targets.Contains(val)) continue;

            _hits.Add(new Hit
            {
                owner = owner,
                ownerContext = context,
                propertyPath = prop.propertyPath,
                referencedValue = val
            });
        }
    }
}
