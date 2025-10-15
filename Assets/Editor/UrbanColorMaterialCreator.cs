using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class UrbanColorMaterialCreator : EditorWindow
{
    private Shader targetShader;
    private string materialFolder = "Assets/UrbanColorMaterials";

    [MenuItem("Tools/Generate Urban Color Materials")]
    public static void ShowWindow()
    {
        GetWindow<UrbanColorMaterialCreator>("Urban Material Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Urban Color Material Creator", EditorStyles.boldLabel);
        targetShader = (Shader)EditorGUILayout.ObjectField("Shader", targetShader, typeof(Shader), false);
        materialFolder = EditorGUILayout.TextField("Material Folder", materialFolder);

        if (GUILayout.Button("Generate Materials"))
        {
            if (targetShader == null)
            {
                EditorUtility.DisplayDialog("Missing Shader", "Please assign a shader.", "OK");
                return;
            }

            CreateMaterials();
        }
    }

    private void CreateMaterials()
    {
        if (!AssetDatabase.IsValidFolder(materialFolder))
        {
            Directory.CreateDirectory(materialFolder);
        }

        Dictionary<string, string[]> colorGroups = new Dictionary<string, string[]>
        {
            {
                "CitySunset", new string[]
                {
                    "#B79467","#584335","#7E6A53","#868D86","#86735C","#211F2B",
                    "#EBC9AF","#85B8B7","#87A1AE","#CCE0A4","#A0B28A","#C2A683"
                }
            },
            {
                "UrbanStreet", new string[]
                {
                    "#C16A2C","#9CB1BE","#F4F3F2","#323333","#9C8778"
                }
            },
            {
                "DenseSkyline", new string[]
                {
                    "#322E22","#292F26","#735B38","#767B93","#A56255","#6E8172",
                    "#FECDA5","#4777A1","#83634F","#E1B16D","#A68064","#A6A364"
                }
            },
            {
                "ElegantStreet", new string[]
                {
                    "#131F25","#727240","#A79878","#E0E0E9","#9C9D9D","#6F4832",
                    "#757674","#A9B1B1","#3C4644","#E7D7C3","#3C6D6B","#E4B592"
                }
            }
        };

        foreach (var group in colorGroups)
        {
            foreach (var hex in group.Value)
            {
                Color color;
                if (ColorUtility.TryParseHtmlString(hex, out color))
                {
                    Material mat = new Material(targetShader);
                    mat.color = color;

                    string name = $"{group.Key}_{hex.TrimStart('#')}";
                    string path = $"{materialFolder}/{name}.mat";

                    AssetDatabase.CreateAsset(mat, path);
                }
                else
                {
                    Debug.LogWarning($"Invalid hex: {hex}");
                }
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Done", "Materials created successfully.", "Nice!");
    }
}
