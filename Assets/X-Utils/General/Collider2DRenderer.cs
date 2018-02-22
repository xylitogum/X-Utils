using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Utils;

[ExecuteInEditMode] //在unity编辑状态下也是可用的
//[RequireComponent(typeof(LineRenderer))]    //要求有这样的一个component的type，会自动创建
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class Collider2DRenderer : MonoBehaviour {
	//private LineRenderer m_lr;
	private EdgeCollider2D m_edge;
	private PolygonCollider2D m_poly;
	private MeshRenderer m_mr;
	private MeshFilter m_mf;
	private Vector2[] m_poses;
	//public Mesh mesh;

	// Use this for initialization
	void Start () {
		//m_lr = GetComponent<LineRenderer>(); //拿到这两个组件
		RegisterComponents();
	}

	void RegisterComponents() {
		m_edge = GetComponent<EdgeCollider2D>();
		m_poly = GetComponent<PolygonCollider2D>();
		m_mr = GetComponent<MeshRenderer>();
		m_mf = GetComponent<MeshFilter>();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_edge) {
			m_poses = m_edge.points;

		}
		else if (m_poly) {
			m_poses = m_poly.points;
		}
		//拿到edge的点的位置用数组储存
        //m_lr.positionCount = m_edge.pointCount;        //数量点传递
        //m_lr.SetPositions(pos);                        //因为是s所以可以批量设置；
		//GenerateMesh();
		/*
		float d = 0;
		Vector3 p0 = m_lr.GetPosition(0);
		for(int i = 0; i <  m_lr.positionCount; i++) {
			Vector3 p1 = m_lr.GetPosition(i); 
			d += (p1-p0).magnitude;
		}
		//Debug.Log(d);
		m_lr.sharedMaterial.SetTextureScale("_MainTex", new Vector2(1,1));
		*/
	}

	public void GenerateMesh() {
		RegisterComponents();
		// Create Vector2 vertices
		Vector2[] vertices2D = m_poses;

		// Use the triangulator to get indices for creating triangles
		Triangulator tr = new Triangulator(vertices2D);
		int[] indices = tr.Triangulate();

		// Create the Vector3 vertices
		Vector3[] vertices = new Vector3[vertices2D.Length];
		for (int i=0; i<vertices.Length; i++) {
			vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
		}

		// Create the mesh
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = indices;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();

		// Set up game object with mesh;

		m_mf.mesh = mesh;
	
	
	}
}
