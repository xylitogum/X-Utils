using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MeshSaverEditor {

	[MenuItem("CONTEXT/MeshFilter/Save Mesh...")]
	public static void SaveMeshInPlace (MenuCommand menuCommand) {
		MeshFilter mf = menuCommand.context as MeshFilter;
		Mesh m = mf.sharedMesh;
		SaveMesh(m, m.name, false, true);
	}

	[MenuItem("CONTEXT/MeshFilter/Save Mesh As New Instance...")]
	public static void SaveMeshNewInstanceItem (MenuCommand menuCommand) {
		MeshFilter mf = menuCommand.context as MeshFilter;
		Mesh m = mf.sharedMesh;
		SaveMesh(m, m.name, true, true);
	}

	public static void SaveMesh (Mesh mesh, string name, bool makeNewInstance, bool optimizeMesh) {
		string path = EditorUtility.SaveFilePanel("Save Separate Mesh Asset", "Assets/", name, "asset");
		if (string.IsNullOrEmpty(path)) return;
        
		path = FileUtil.GetProjectRelativePath(path);

		Mesh meshToSave = (makeNewInstance) ? Object.Instantiate(mesh) as Mesh : mesh;
		
		if (optimizeMesh)
		     MeshUtility.Optimize(meshToSave);
        
		AssetDatabase.CreateAsset(meshToSave, path);
		AssetDatabase.SaveAssets();
	}
	
	
	[MenuItem("CONTEXT/MeshFilter/Print Mesh...")]
	public static void PrintMesh (MenuCommand menuCommand) {
		MeshFilter mf = menuCommand.context as MeshFilter;
		Mesh m = mf.sharedMesh;
		Debug.Log("Printing Mesh '" + m.name +"'");
		
		for (int i = 0; i < m.vertices.Length; i++)
		{
			Debug.Log("Vertices["+ i +"]: "+ m.vertices[i] + "\n" +
					"normal: "+ m.normals[i]+ "\n" +
			        "uv: "+ m.uv[i] );
		}
		
		
		for (int i = 0; i < m.triangles.Length/3; i++)
		{
			Debug.Log("Triangles["+ i +"]: "+ m.triangles[i*3] + "," + m.triangles[i*3+1] + "," + m.triangles[i*3+2]);
			Debug.Log("= vertices: "+ m.vertices[m.triangles[i*3]] + "\n" +
			          "= vertices: "+ m.vertices[m.triangles[i*3 + 1]] + "\n" +
			          "= vertices: "+ m.vertices[m.triangles[i*3 + 2]]);
		}
	}
}
