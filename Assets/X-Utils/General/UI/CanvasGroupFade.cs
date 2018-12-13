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
	public class CanvasGroupFade : MonoBehaviour
	{

		[System.Serializable]
		public class DisplayState
		{
			public string name;
			[Tooltip("The List of Canvas Groups to fade.")]
			public CanvasGroup[] stateGroups;
			public UnityEvent onStateStartEvent;
			public UnityEvent onStateEndEvent;
		}
		

		#region EXTERNAL_FIELDS

		[Header("CrossFade Settings")]
		public DisplayState[] states;
		protected DisplayState currentState;
		
		[Tooltip("Determines how long the crossfade takes.")]
		[Range(0f, 10f)]
		public float duration = 1f;

		
		[Header("Advanced Settings")]
		//[Tooltip("Check this box if you wish this UI element to be trackable by mouse or other raycast inputs during the crossfade.")]
		//public bool setRaycastTarget = false;
		public AnimationCurve crossfadeCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
		[Tooltip("Check this box when you don't want the crossfade timer to be affected by TimeScale.")]
		public bool ignoreTimeScale = false;
		
		#endregion
		
		#region INTERNAL_FIELDS
		protected bool fading = false;
		protected float currentTimer = 0f; // the current timer that accumluates as the crossfade goes.
		
		#endregion
		
		
		#region MONOBEHAVIOUR
		// Use this for initialization
		void Awake()
		{
			currentState = states[0];
		}

		void Start()
		{
			
			
		}

		private void OnEnable()
		{
			
		}
		
		private void OnDisable()
		{
		}

		// Update is called once per frame
		void Update()
		{
			if (fading)
			{
				// Timer Increment
				if (currentTimer >= duration || duration < 0f) // Timer Expires
				{
					currentTimer = 0f;
					fading = false;
					UpdateCanvasGroups(1f);
				}
				else
				{
					currentTimer += ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
					// Update CrossFade Mix
					float ratio = Mathf.Clamp01(currentTimer / duration);
					UpdateCanvasGroups(ratio);
					

				}
			}
			
		}


		#endregion

		public void SwitchTo(int stateIndex)
		{
			if (stateIndex >= states.Length) return;
			DisplayState state = states[stateIndex];
			if (currentState == state) return;
			
			fading = true;
			currentTimer = 0f;
			SetDisplayState(state);
		}
		
		
		void UpdateCanvasGroups(float ratio)
		{
			foreach (var inGroup in GetInStateGroups())
			{
				inGroup.alpha = crossfadeCurve.Evaluate(ratio);	
			}
			foreach (var notInGroup in GetNotInStateGroups())
			{
				notInGroup.alpha = crossfadeCurve.Evaluate(1f - ratio);	
			}
		}

		
		void OnStateStart(DisplayState state)
		{
			foreach (var stateGroup in state.stateGroups)
			{
				stateGroup.gameObject.SetActive(true);
				stateGroup.blocksRaycasts = true;
			}
			state.onStateStartEvent.Invoke();
		}
		
		void OnStateEnd(DisplayState state)
		{
			foreach (var stateGroup in state.stateGroups)
			{
				stateGroup.blocksRaycasts = false;
			}
			state.onStateEndEvent.Invoke();
		}

		CanvasGroup[] GetInStateGroups()
		{
			return currentState.stateGroups;
		}

		CanvasGroup[] GetNotInStateGroups()
		{
			List<CanvasGroup> results = new List<CanvasGroup>();
			foreach (var state in states)
			{
				if (currentState != state)
				{
					results.AddRange(state.stateGroups);
				}
			}

			return results.ToArray();
		}
		
		public void SetDisplayState(DisplayState newState)
		{
			if (currentState == newState) return;
			OnStateEnd(currentState);
			OnStateStart(newState);
			currentState = newState;
		}

	}
}