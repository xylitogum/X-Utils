using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshDeformer : MonoBehaviour
{
	
	private bool sleeping = true;
	public bool canSleep = true;
	public float sleepThreshold = 0.05f;
	private float sleepSqrThreshold;
	
	public float forceMultiplier = 1f;
	public float springForce = 20f;
	public float damping = 5f;
	private Mesh deformingMesh;
	private Vector3[] originalVertices;
	private Vector3[] displacedVertices;
	private Vector3[] vertexVelocities;
	private float uniformScale = 1f;

	void Awake()
	{
		sleepSqrThreshold = sleepThreshold * sleepThreshold;
		deformingMesh = GetComponent<MeshFilter>().mesh;
		originalVertices = deformingMesh.vertices;
		displacedVertices = new Vector3[originalVertices.Length];
		for (int i = 0; i < originalVertices.Length; i++) {
			displacedVertices[i] = originalVertices[i];
		}
		vertexVelocities = new Vector3[originalVertices.Length];
	}
	
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	void Update () {
		
		uniformScale = (transform.localScale.x);
		if (!canSleep || !sleeping)
		{
			bool changedAny = false;
			for (int i = 0; i < displacedVertices.Length; i++) {
				bool changedThis = UpdateVertex(i);
				changedAny = changedAny || changedThis;
			}

			if (changedAny)
			{
				AssignVertices();
			}
			else
			{
				if (canSleep) sleeping = true;
			}
			
		}
		
	}


	void AssignVertices()
	{
		deformingMesh.vertices = displacedVertices;
		//deformingMesh.RecalculateNormals();
		deformingMesh.RecalculateTangents();
	}
	
	
	public void AddDeformingForce (Vector3 point, float force)
	{
		Debug.Log("AddDeformingForce: " + point + " * " + force);
		sleeping = false;
		point = transform.InverseTransformPoint(point);
		//Debug.DrawLine(Camera.main.transform.position, point);
		for (int i = 0; i < displacedVertices.Length; i++) {
			AddForceToVertex(i, point, force);
		}
	}
	
	void AddForceToVertex (int i, Vector3 point, float force)
	{
		sleeping = false;
		Vector3 pointToVertex = displacedVertices[i] - point;
		pointToVertex *= uniformScale;
		float attenuatedForce = forceMultiplier * force / (1f + pointToVertex.sqrMagnitude);
		
		float deltaVelocity = attenuatedForce * Time.deltaTime;
		vertexVelocities[i] += pointToVertex.normalized * deltaVelocity;
		
	}
	
	bool UpdateVertex (int i) {
		Vector3 velocity = vertexVelocities[i];
		Vector3 displacement = displacedVertices[i] - originalVertices[i];
		displacement *= uniformScale;
		velocity -= displacement * springForce * Time.deltaTime;
		velocity *= 1f - damping * Time.deltaTime;
		vertexVelocities[i] = velocity;
		displacedVertices[i] += velocity * (Time.deltaTime / (uniformScale > 0f ? uniformScale : 0.0001f));
		
		bool changedAny = (displacement.sqrMagnitude > sleepSqrThreshold) || (velocity.sqrMagnitude > sleepSqrThreshold);
		//if (changedAny) Debug.Log(displacement.sqrMagnitude + ", " + velocity.sqrMagnitude);
		return changedAny;
	}
	
	
}
