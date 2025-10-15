using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ShaderMaterialFinder : EditorWindow
{
    Shader targetShader;
    List<Material> foundMaterials = new List<Material>();
    Vector2 scrollPos;
    string searchQuery = "";

    [MenuItem("Tools/Find Materials by Shader")]
    static void ShowWindow()
    {
        GetWindow<ShaderMaterialFinder>("Shader Material Finder");
    }

    void OnGUI()
    {
        GUILayout.Label("Find Materials Using a Shader", EditorStyles.boldLabel);
        targetShader = EditorGUILayout.ObjectField("Target Shader", targetShader, typeof(Shader), false) as Shader;

        if (GUILayout.Button("Find Materials"))
        {
            FindMaterials();
        }

        if (foundMaterials.Count > 0)
        {
            EditorGUILayout.Space();
            searchQuery = EditorGUILayout.TextField("Search", searchQuery);
            EditorGUILayout.Space();

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(400));
            foreach (var mat in foundMaterials)
            {
                if (!string.IsNullOrEmpty(searchQuery) && !mat.name.ToLower().Contains(searchQuery.ToLower()))
                    continue;

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(AssetPreview.GetAssetPreview(mat) ?? Texture2D.grayTexture, GUILayout.Width(50), GUILayout.Height(50));
                EditorGUILayout.ObjectField(mat, typeof(Material), false);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndScrollView();
        }
    }

    void FindMaterials()
    {
        foundMaterials.Clear();

        string[] guids = AssetDatabase.FindAssets("t:Material");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat != null && mat.shader == targetShader)
            {
                foundMaterials.Add(mat);
            }
        }

        Debug.Log($"Found {foundMaterials.Count} materials using {targetShader.name}.");
    }
}
