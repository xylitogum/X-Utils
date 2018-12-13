using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	public class RectTransformFollower : MonoBehaviour
	{
		public RectTransform target;
		public bool updateSize = false;

		private RectTransform _rectTransform;
		// Use this for initialization
		private void OnEnable()
		{
			_rectTransform = GetComponent<RectTransform>();
		}

		private void OnDisable()
		{
			_rectTransform = null;
		}

		// Update is called once per frame
		void Update()
		{
			if (_rectTransform && target)
			{
				
				_rectTransform.anchorMin = target.anchorMin;
				_rectTransform.anchorMax = target.anchorMax;

				_rectTransform.anchoredPosition  = target.anchoredPosition;
				_rectTransform.pivot = target.pivot;
				_rectTransform.transform.position = target.transform.position;
				_rectTransform.transform.rotation = target.transform.rotation;
				
				if (updateSize)
				{
					_rectTransform.SetSize(target.GetSize());
				}
			}
		}
	}
}