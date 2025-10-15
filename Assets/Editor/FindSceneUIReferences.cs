using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System;
using System.Linq;
using System.Collections.Generic;

public class FindSceneUIReferences : EditorWindow
{
    private Sprite spriteToFind;
    private TMP_FontAsset fontToFind;
    private string searchText = "";
    private MonoScript scriptToFind;
    private GameObject prefabToFind;
    private Material materialToFind;
    private Texture textureToFind;
    private AudioClip audioClipToFind;
    private Mesh meshToFind;

    private Vector2 scrollPos;
    private List<GameObject> foundObjects = new List<GameObject>();

    [MenuItem("Tools/Find UI References In Scene")]
    public static void ShowWindow()
    {
        GetWindow<FindSceneUIReferences>("Find Scene UI References");
    }

    private void OnGUI()
    {
        GUILayout.Label("Find References in Current Scene", EditorStyles.boldLabel);

        spriteToFind = (Sprite)EditorGUILayout.ObjectField("Sprite", spriteToFind, typeof(Sprite), false);
        fontToFind = (TMP_FontAsset)EditorGUILayout.ObjectField("TMP Font", fontToFind, typeof(TMP_FontAsset), false);
        searchText = EditorGUILayout.TextField("Text Content", searchText);
        scriptToFind = (MonoScript)EditorGUILayout.ObjectField("Script", scriptToFind, typeof(MonoScript), false);
        prefabToFind = (GameObject)EditorGUILayout.ObjectField("Prefab", prefabToFind, typeof(GameObject), false);
        materialToFind = (Material)EditorGUILayout.ObjectField("Material", materialToFind, typeof(Material), false);
        textureToFind = (Texture)EditorGUILayout.ObjectField("Texture", textureToFind, typeof(Texture), false);
        audioClipToFind = (AudioClip)EditorGUILayout.ObjectField("Audio Clip", audioClipToFind, typeof(AudioClip), false);
        meshToFind = (Mesh)EditorGUILayout.ObjectField("Mesh", meshToFind, typeof(Mesh), false);

        GUILayout.Space(10);

        if (GUILayout.Button("Search in Scene"))
        {
            SearchInScene();
        }

        if (foundObjects.Count > 0 && GUILayout.Button("Clear Results"))
        {
            foundObjects.Clear();
        }

        GUILayout.Space(10);

        if (foundObjects.Count > 0)
        {
            GUILayout.Label("Found Objects:", EditorStyles.boldLabel);

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(200));

            foreach (GameObject obj in foundObjects)
            {
                if (obj == null) continue;

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.ObjectField(obj.name, obj, typeof(GameObject), true);

                if (GUILayout.Button("Ping", GUILayout.Width(40)))
                {
                    EditorGUIUtility.PingObject(obj);
                }

                if (GUILayout.Button("Select", GUILayout.Width(50)))
                {
                    Selection.activeGameObject = obj;
                }

                if (GUILayout.Button("Frame", GUILayout.Width(50)))
                {
                    EditorGUIUtility.PingObject(obj);
                    Selection.activeGameObject = obj;
                    SceneView.lastActiveSceneView.FrameSelected();
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
        }
    }

    void SearchInScene()
    {
        foundObjects.Clear();

        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        Type scriptType = scriptToFind != null ? scriptToFind.GetClass() : null;

        foreach (GameObject rootObj in rootObjects)
        {
            GameObject[] allObjects = rootObj.GetComponentsInChildren<Transform>(true)
                                             .Select(t => t.gameObject)
                                             .ToArray();

            foreach (GameObject obj in allObjects)
            {
                bool matched = false;
                Component[] components = obj.GetComponents<Component>();

                foreach (Component comp in components)
                {
                    if (comp == null) continue;

                    // Sprite
                    if (!matched && spriteToFind && comp is Image img && img.sprite == spriteToFind)
                    {
                        matched = true;
                    }

                    // TMP Font + Text
                    if (!matched && comp is TMP_Text tmp)
                    {
                        if (fontToFind && tmp.font == fontToFind)
                            matched = true;

                        if (!string.IsNullOrEmpty(searchText) && tmp.text.Contains(searchText))
                            matched = true;
                    }

                    // Script
                    if (!matched && scriptType != null && comp.GetType() == scriptType)
                    {
                        matched = true;
                    }

                    // AudioClip
                    if (!matched && audioClipToFind && comp is AudioSource audio && audio.clip == audioClipToFind)
                    {
                        matched = true;
                    }

                    // Renderer-based (Materials, Textures)
                    if (!matched && comp is Renderer renderer)
                    {
                        if (materialToFind && renderer.sharedMaterials.Any(mat => mat == materialToFind))
                            matched = true;

                        if (textureToFind)
                        {
                            foreach (var mat in renderer.sharedMaterials)
                            {
                                if (mat != null && mat.mainTexture == textureToFind)
                                {
                                    matched = true;
                                    break;
                                }
                            }
                        }
                    }

                    // Mesh
                    if (!matched && meshToFind)
                    {
                        if (comp is MeshFilter mf && mf.sharedMesh == meshToFind)
                            matched = true;

                        if (comp is SkinnedMeshRenderer smr && smr.sharedMesh == meshToFind)
                            matched = true;
                    }
                }

                // Prefab Reference
                if (!matched && prefabToFind != null)
                {
                    GameObject prefabRoot = PrefabUtility.GetCorrespondingObjectFromSource(obj);
                    if (prefabRoot != null && prefabRoot == prefabToFind)
                    {
                        matched = true;
                    }
                }

                if (matched)
                {
                    foundObjects.Add(obj);
                    Debug.Log($"🔍 Found match on '{obj.name}'", obj);
                }
            }
        }

        if (foundObjects.Count == 0)
        {
            Debug.Log("❌ No references found in the current scene.");
        }
    }
}
