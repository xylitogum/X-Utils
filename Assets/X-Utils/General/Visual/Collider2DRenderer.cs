using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace X_Utils.Visual {

  [ExecuteInEditMode] //在unity编辑状态下也是可用的
  [RequireComponent(typeof(LineRenderer))]
  [RequireComponent(typeof(Collider2D))]
  //[RequireComponent(typeof(MeshRenderer))]
  //[RequireComponent(typeof(MeshFilter))]
  public class Collider2DRenderer : MonoBehaviour
  {
      private LineRenderer m_lr;
      private EdgeCollider2D m_edge;
      private PolygonCollider2D m_poly;
      //private MeshRenderer m_mr;
      //private MeshFilter m_mf;
      private Vector2[] m_poses;
      //public Mesh mesh;

      // Use this for initialization
      void Start()
      {
          //m_lr = GetComponent<LineRenderer>(); //拿到这两个组件
          RegisterComponents();
      }

      void RegisterComponents()
      {
          m_edge = GetComponent<EdgeCollider2D>();
          m_poly = GetComponent<PolygonCollider2D>();
          m_lr = GetComponent<LineRenderer>();
          //m_mr = GetComponent<MeshRenderer>();
          //m_mf = GetComponent<MeshFilter>();
      }

      private void OnEnable()
      {
          RegisterComponents();
      }

      private void OnDisable()
      {
          m_edge = null;
          m_poly = null;
      }

      // Update is called once per frame
      void Update()
      {
          int count = 0;
          if (m_edge)
          {
              m_poses = m_edge.points;
              count = m_edge.pointCount;
          }
          else if (m_poly)
          {
              m_poses = m_poly.points;
              count = m_poly.points.Length;
              m_lr.loop = true;
          }
          //拿到edge的点的位置用数组储存
          m_lr.positionCount = count;
          m_lr.useWorldSpace = false;
          Vector3[] poses3d = System.Array.ConvertAll<Vector2, Vector3>(m_poses, v => new Vector3(v.x, v.y, 0f));
          m_lr.SetPositions(poses3d);                        //因为是s所以可以批量设置；
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

  }

}
