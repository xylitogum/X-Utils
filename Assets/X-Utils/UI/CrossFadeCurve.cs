using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace X_Utils.UI
{
	
	/// <summary>
	/// Represents a component that controls the crossfade process of UI elements.
	/// </summary>
	[RequireComponent(typeof(MaskableGraphic))]
	[RequireComponent(typeof(RectTransform))]
	public class CrossFadeCurve : MonoBehaviour
	{
		
		public enum MixMode
		{
			Overwrite, // The curve or gradient value will overwrite initial color or size value when crossfading.
			Multiply // the curve or gradient value will multiply initial value when cross fading.
		}

		public enum ActivationMode
		{
			Manual, // You need to call the Activate() method in order to start the cross fade.
			OnStart, // CrossFade activates automatically when it is loaded.
		}
		

		#region EXTERNAL_FIELDS
		[Header("CrossFade Settings")]
		[Tooltip("Determines how long the crossfade takes.")]
		[Range(0f, 10f)]
		public float duration = 1f;
		[Tooltip("Determines how the color of this UI element will change during the crossfade.")]
		public Gradient colorOverLifeTime;
		[Tooltip("Determines how the size of this UI element will change during the crossfade.")]
		public AnimationCurve sizeOverLifeTime = AnimationCurve.Constant(0f, 1f, 1f);
		[Tooltip("Determines whether the CrossFade curves will multiply the initial values, or will overwrite them.")]
		public MixMode mixMode = MixMode.Multiply;
		[Tooltip("Determines how the crossfade is activated, either by manually calling the Activate() function, or self activates on start.")]
		public ActivationMode activationMode = ActivationMode.OnStart;
		
		[Header("Advanced Settings")]
		[Tooltip("Check this box if you wish this UI element to be trackable by mouse or other raycast inputs during the crossfade.")]
		public bool enableRaycastTarget = false;
		[Tooltip("Check this box when you don't want the crossfade timer to be affected by TimeScale.")]
		public bool ignoreTimeScale = false;
		[Tooltip("These actions will be invoked when the crossfade activates.")]
		public UnityEvent onActivatedEvent;
		[Tooltip("These actions will be invoked when the crossfade finishes.")]
		public UnityEvent onFinishedEvent;


		[ContextMenu("Set to Fade In")]
		void TemplateFadeIn()
		{
			// Set Gradient to Fade In White
			Gradient g = new Gradient();
			GradientColorKey[] gck = new GradientColorKey[2];
			gck[0].color = Color.white;
			gck[0].time = 0f;
			gck[1].color = Color.white;
			gck[1].time = 1f;
			GradientAlphaKey[] gak = new GradientAlphaKey[2];
			gak[0].alpha = 0f;
			gak[0].time = 0f;
			gak[1].alpha = 1f;
			gak[1].time = 1f;
			g.SetKeys(gck, gak);
			colorOverLifeTime = g;
			
			sizeOverLifeTime = AnimationCurve.Constant(0f, 1f, 1f);
			mixMode = MixMode.Multiply;
			activationMode = ActivationMode.OnStart;
			enableRaycastTarget = false;
		}
		
		[ContextMenu("Set to Fade Out")]
		void TemplateFadeOut()
		{
			
			// Set Gradient to Fade In White
			Gradient g = new Gradient();
			GradientColorKey[] gck = new GradientColorKey[2];
			gck[0].color = Color.white;
			gck[0].time = 0f;
			gck[1].color = Color.white;
			gck[1].time = 1f;
			GradientAlphaKey[] gak = new GradientAlphaKey[2];
			gak[0].alpha = 1f;
			gak[0].time = 0f;
			gak[1].alpha = 0f;
			gak[1].time = 1f;
			g.SetKeys(gck, gak);
			colorOverLifeTime = g;
			
			
			sizeOverLifeTime = AnimationCurve.Constant(0f, 1f, 1f);
			mixMode = MixMode.Multiply;
			activationMode = ActivationMode.Manual;
			enableRaycastTarget = false;
			
			onFinishedEvent = new UnityEvent();
			UnityEditor.Events.UnityEventTools.AddPersistentListener (onFinishedEvent, () => { gameObject.SetActive(false); });
		}
		
		
		#endregion
		
		#region INTERNAL_FIELDS
		protected bool activated = false; // determines whether the crossfade is in effect or not.
		protected float currentTimer = 0f; // the current timer that accumluates as the crossfade goes.
		private Vector3 _startSize; // stores the size information on start.
		private Color _startColor; // stores the color information on start.
		private bool _startRaycastTarget; // stores the raycastable information on start.
		
		protected MaskableGraphic _graphic;
		protected RectTransform _rectTransform;
		#endregion
		
		
		#region MONOBEHAVIOUR
		// Use this for initialization
		void Awake()
		{
			_graphic = GetComponent<MaskableGraphic>();
			_rectTransform = GetComponent<RectTransform>();
		}

		void Start()
		{
			_startColor = _graphic.color;
			_startRaycastTarget = _graphic.raycastTarget;
			_startSize = _rectTransform.localScale;
			if (activationMode == ActivationMode.OnStart)
			{
				Activate();
			}
		}
		
		// Update is called once per frame
		void Update()
		{
			if (activated)
			{
				// Timer Increment
				currentTimer += ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
				if (currentTimer >= duration || duration < 0f) // Timer Expires
				{
					currentTimer = 0f;
					activated = false;
					OnFinished();
				}
				else
				{
					// Update CrossFade Mix
					float ratio = Mathf.Clamp01(currentTimer / duration);
					UpdateCrossFadeMix(ratio);

				}
			}
			
		}


		#endregion
		
		
		
		/// <summary>
		/// Updates the crossfading state of the UI element by given ratio.
		/// </summary>
		/// <param name="ratio">CrossFade Status, from 0 to 1</param>
		void UpdateCrossFadeMix(float ratio)
		{
			switch (mixMode)
			{
				case MixMode.Multiply:
					_graphic.color = _startColor * colorOverLifeTime.Evaluate(ratio);
					_rectTransform.localScale = Vector3.Scale( _startSize, Vector3.one * sizeOverLifeTime.Evaluate(ratio));
					break;
				case MixMode.Overwrite:
					_graphic.color = colorOverLifeTime.Evaluate(ratio);
					_rectTransform.localScale = Vector3.one * sizeOverLifeTime.Evaluate(ratio);
					break;
				default:
					break;
			}
		}
		
		/// <summary>
		/// Starts the CrossFader.
		/// </summary>
		public void Activate()
		{
			currentTimer = 0f;
			activated = true;
			_graphic.raycastTarget = enableRaycastTarget;
			
			onActivatedEvent.Invoke();
		}
		
		/// <summary>
		/// Resets the CrossFader to the initial state as if it doesn't exist.
		/// </summary>
		public void OnFinished()
		{
			activated = false;
			currentTimer = 0f;
			UpdateCrossFadeMix(1f);
			_graphic.raycastTarget = _startRaycastTarget;
			
			onFinishedEvent.Invoke();
		}

		/// <summary>
		/// Resets the CrossFader to the initial state as if it doesn't exist.
		/// </summary>
		public void ResetCrossFade()
		{
			activated = false;
			currentTimer = 0f;
			_graphic.color = _startColor;
			_rectTransform.localScale = _startSize;
			_graphic.raycastTarget = _startRaycastTarget;
		}

	}
}
