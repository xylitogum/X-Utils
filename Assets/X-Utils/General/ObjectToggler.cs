using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToggler : MonoBehaviour {

	public void Toggle()
	{
		if (gameObject.activeSelf)
		{
			gameObject.SetActive(false);
			//return false;
		}
		else
		{
			gameObject.SetActive(true);
			//return true;
		}

	}
	
	
}
