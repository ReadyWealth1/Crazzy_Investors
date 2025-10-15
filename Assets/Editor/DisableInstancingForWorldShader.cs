using UnityEngine;
using UnityEditor;

public class DisableInstancingForWorldShader : EditorWindow
{
    [MenuItem("Tools/Fix All: Disable Instancing on WorldnewShader Materials")]
    static void DisableGPUInstancing()
    {
        string shaderName = "Shader Graphs/WorldnewShader";
        int changedCount = 0;

        string[] materialGuids = AssetDatabase.FindAssets("t:Material");

        foreach (string guid in materialGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat != null && mat.shader != null && mat.shader.name == shaderName)
            {
                if (mat.enableInstancing)
                {
                    mat.enableInstancing = false;
                    EditorUtility.SetDirty(mat);
                    changedCount++;
                }
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"✅ Disabled GPU Instancing on {changedCount} materials using '{shaderName}'.");
    }
}
