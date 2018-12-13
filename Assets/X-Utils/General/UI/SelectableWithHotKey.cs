using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace X_Utils.UI
{
	[RequireComponent(typeof(Selectable))]
	public class SelectableWithHotKey : MonoBehaviour
	{
		private Selectable _selectable;
		public KeyCode hotkey;
		
		void Awake()
		{
			_selectable = GetComponent<Selectable>();
		}
		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
			
			if (Input.GetKeyDown(hotkey) && _selectable.IsInteractable())
			{
				if (_selectable is Button)
				{
					((Button)_selectable).onClick.Invoke();
				}
				else if (_selectable is Toggle)
				{
					((Toggle) _selectable).isOn = !((Toggle) _selectable).isOn;
					((Toggle)_selectable).onValueChanged.Invoke(((Toggle)_selectable).isOn);
				}
				
				
			}
		}
	}
	
}

