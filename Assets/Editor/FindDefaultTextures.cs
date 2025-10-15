using UnityEngine;
using UnityEditor;

public class OptimizeTexturesForWebGL : EditorWindow
{
    [MenuItem("Tools/WebGL Texture Optimizer (25MB Limit)")]
    static void Optimize()
    {
        string[] guids = AssetDatabase.FindAssets("t:Texture2D");
        int fixedCount = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var importer = AssetImporter.GetAtPath(path) as TextureImporter;

            if (importer != null && importer.textureType == TextureImporterType.Default)
            {
                var settings = importer.GetPlatformTextureSettings("WebGL");
                settings.overridden = true;
                settings.maxTextureSize = 256;
                settings.textureCompression = TextureImporterCompression.Compressed;
                settings.format = TextureImporterFormat.ETC2_RGB4; // Or ETC2_RGB8 if supported
                importer.SetPlatformTextureSettings(settings);

                importer.wrapMode = TextureWrapMode.Clamp;
                importer.filterMode = FilterMode.Bilinear;
                importer.mipmapEnabled = false;
                importer.anisoLevel = 4;

                importer.SaveAndReimport();
                fixedCount++;
            }
        }

        Debug.Log($"✅ Optimized {fixedCount} textures for WebGL (max 256px, compressed).");
    }
}
