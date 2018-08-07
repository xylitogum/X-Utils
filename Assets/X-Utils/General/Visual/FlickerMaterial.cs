using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace X_Utils.Visual
{
	[RequireComponent(typeof(Renderer))]
	public class FlickerMaterial : MonoBehaviour {
		private Renderer m_renderer;
		private Material m_mat;

		[MinMaxSlider(0f, 10f)]
		public Vector2 flickerRange = new Vector2(0.5f, 2f);

		public FlickerType flickerType = FlickerType.Random;

		public enum FlickerType
		{
			Random,
			Periodic
		}

		[Range(0f, 1f)]
		public float smoothRate = 0.05f;
		[Range(0f, 10f)]
		public float periodTime = 3f;



		public float flickerMultiplier = 1f;
		private Color initialColor;
		// Use this for initialization
		void OnEnable () {
			m_renderer = GetComponent<Renderer>();
			m_mat = m_renderer.material;
			initialColor = m_mat.GetColor("_EmissionColor");
			//initialIntensity = m_light.intensity;
		}

		void OnDisable()
		{

		}

		// Update is called once per frame
		void Update ()
		{

			if (flickerType == FlickerType.Random)
			{
				float targetFlickerMultiplier = Random.Range(flickerRange.x, flickerRange.y);
				flickerMultiplier = Mathf.Lerp(flickerMultiplier, targetFlickerMultiplier, smoothRate);
			}
			else if (flickerType == FlickerType.Periodic)
			{
				flickerMultiplier = Mathf.Sin(Time.realtimeSinceStartup / periodTime * 2 * Mathf.PI)
				                    * (flickerRange.y - flickerRange.x) + flickerRange.x;
			}

			Vector3 hsv = Vector3.zero;
			Color.RGBToHSV(initialColor, out hsv.x, out hsv.y, out hsv.z);
			hsv.z *= flickerMultiplier;
			Color finalColor = Color.HSVToRGB(hsv.x, hsv.y, hsv.z);
			m_mat.SetColor ("_EmissionColor", finalColor);

		}
	}

}
