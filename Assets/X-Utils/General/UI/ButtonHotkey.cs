using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class ButtonHotkey : MonoBehaviour
{
	public KeyCode hotkey;
	private Button _button;
	//private Color _outlineInitialColor;

	void Awake()
	{
		_button = GetComponent<Button>();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(hotkey))
		{
			_button.onClick.Invoke();
			
		}
		
	}
}
