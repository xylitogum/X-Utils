using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The Life time script that automatically destroys object after a certain period of time.
/// </summary>
public class LifeTime : MonoBehaviour {
	
	[Tooltip("This Object will be destroyed after the given duration time (in seconds).")]
	public float duration;
	private float timeStamp_birth;

	// Use this for initialization
	public void Start () {
		timeStamp_birth = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Time.time >= timeStamp_birth + duration) {
			Destroy(this.gameObject);
		}

	}
}
