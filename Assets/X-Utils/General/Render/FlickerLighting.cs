using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace X_Utils.Visual
{
	[RequireComponent(typeof(Light))]
	[ExecuteInEditMode]
	public class FlickerLighting : MonoBehaviour {
		private Light m_light;
		[Range(0f, 1f)]
		public float smoothRate = 0.05f;
		[MinMaxSlider(0f, 10f)]
		public Vector2 flickerRange = new Vector2(0.5f, 2f);
		public float flickerMultiplier = 1f;
		public float initialIntensity = 2f;
		// Use this for initialization
		void OnEnable () {
			m_light = GetComponent<Light>();
			//initialIntensity = m_light.intensity;
		}

		void OnDisable()
		{

		}

		// Update is called once per frame
		void Update () {
			float targetFlickerMultiplier = Random.Range(flickerRange.x, flickerRange.y);
			flickerMultiplier = Mathf.Lerp(flickerMultiplier, targetFlickerMultiplier, smoothRate);
			m_light.intensity = initialIntensity * flickerMultiplier;

		}
	}
}
