using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldspaceBillboard : MonoBehaviour
{

	public bool useWorldUp = false;
	public bool parallexOnly = true;
	public Vector3 offset;

	private Vector3 _initialLocalPositon;
	// Use this for initialization
	void Start ()
	{
		_initialLocalPositon = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		Transform cameraTransform = Camera.main.transform;
		transform.localPosition = _initialLocalPositon + cameraTransform.rotation * offset;
		if (parallexOnly)
		{
			//Vector3 rotEuler = Quaternion.LookRotation(cameraTransform.forward, useWorldUp ? Vector3.up : cameraTransform.up).eulerAngles;
			//Quaternion rot = Quaternion.Euler(0f, rotEuler.y, 0f);
			transform.rotation =  cameraTransform.rotation;
		}
		else
		{
			transform.rotation = Quaternion.LookRotation(
				transform.position - cameraTransform.position,
				useWorldUp ? Vector3.up : cameraTransform.up);
		}
		
	}
	
	
	
}
