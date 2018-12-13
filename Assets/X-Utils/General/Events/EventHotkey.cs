using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventHotkey : MonoBehaviour
{
	public KeyCode hotkey;
	public bool mouseKey = false;
	public UnityEvent onKeyEvent;
	
	//private Color _outlineInitialColor;

	void Awake()
	{
		
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(hotkey) || (mouseKey && Input.GetMouseButtonDown(0)))
		{
			onKeyEvent.Invoke();
			
		}
		
	}
}
