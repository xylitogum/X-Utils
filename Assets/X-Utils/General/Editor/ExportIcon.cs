using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;



public class ExportIcon
{

    
    [MenuItem("Assets/Export Preview Icon", false, 40)]
    private static void ExportPreviewIcon()
    {
        string selectionObjectPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        Texture2D texture = AssetPreview.GetAssetPreview(Selection.activeObject); // AssetDatabase.GetCachedIcon(selectionObjectPath);
        
        string savePath = EditorUtility.SaveFilePanel("Save Icon Asset", Directory.GetParent(selectionObjectPath).FullName, Selection.activeObject.name, "png");
        if (string.IsNullOrEmpty(savePath)) return;
        
        savePath = FileUtil.GetProjectRelativePath(savePath);
        AssetDatabase.CreateAsset(new Texture2D(1,1), savePath);
        byte[] fileBytes = texture.EncodeToPNG();
        File.WriteAllBytes(savePath, fileBytes);
        /*
        FileStream stream = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.Write);
        BinaryWriter writer = new BinaryWriter(stream);
        for (int i = 0; i < fileBytes.Length; i++) {
            writer.Write(fileBytes[i]);
        }
        writer.Close();
        stream.Close();
        */

        AssetDatabase.ImportAsset(savePath, ImportAssetOptions.ForceUpdate);
        //AssetDatabase.Refresh();
        //AssetDatabase.SaveAssets();
        
        TextureImporter ti = AssetImporter.GetAtPath (savePath) as TextureImporter;
        ti.mipmapEnabled = false;
        ti.textureType = TextureImporterType.Sprite;
        EditorUtility.SetDirty (ti);
        ti.SaveAndReimport ();
    }
 
    
    [MenuItem("Assets/Export Preview Icon", true)]
    private static bool ExportPreviewValidation()
    {
        // This returns true when the selected object is a Material (the menu item will be disabled otherwise).
        return Selection.activeObject.GetType() == typeof(Material);
    }
    
    
    
}

