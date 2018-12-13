using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[RequireComponent(typeof(MeshDeformer))]
[RequireComponent(typeof(Rigidbody))]
public class CollisionDeformer : MonoBehaviour
{
	
	public bool divideByMass = true;
	public float minForceOffset = 0.05f;
	private Rigidbody _rigidbody;
	private MeshDeformer _meshDeformer;


	void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_meshDeformer = GetComponent<MeshDeformer>();
	}
	
	
	// Use this for initialization
	void Start () {
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		float force = collision.impulse.magnitude;
		if (divideByMass)
		{
			force /= _rigidbody.mass;
		}
		foreach (ContactPoint contactPoint in collision.contacts)
		{
			Vector3 point = contactPoint.point - contactPoint.normal * Mathf.Max(contactPoint.separation, minForceOffset);
			_meshDeformer.AddDeformingForce(point, force);
			
			
		}
	}
	
	private void OnCollisionStay(Collision collision)
	{
		float force = collision.impulse.magnitude;
		if (divideByMass)
		{
			force /= _rigidbody.mass;
		}
		foreach (ContactPoint contactPoint in collision.contacts)
		{
			Vector3 point = contactPoint.point - contactPoint.normal * Mathf.Max(contactPoint.separation, minForceOffset);
			_meshDeformer.AddDeformingForce(point, force);
			
			
		}
	}
}
