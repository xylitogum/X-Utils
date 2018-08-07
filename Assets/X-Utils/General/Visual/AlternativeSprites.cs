using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace X_Utils.Visual
{
	[ExecuteInEditMode]
	public class AlternativeSprites : MonoBehaviour
	{

		public bool randomizeRotation = false;


		[ConditionalHide("randomizeRotation", true, order = 0)]
		//[MinMaxSlider(0f, 360f, order = 1)]
		public Vector2 rotationRange = new Vector2(0f, 360f);

		[SerializeField]
		public List<Sprite> sprites;


		private SpriteRenderer _spriteRenderer;

		void OnEnable()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
#if UNITY_EDITOR
			if (sprites.Count > 0)
			{
				_spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
			}

			if (randomizeRotation)
			{
				transform.localRotation = Quaternion.Euler(
					transform.localRotation.eulerAngles.x,
					Random.Range(rotationRange.x, rotationRange.y),
					transform.localRotation.eulerAngles.z);
			}
#endif

		}



	}
}
