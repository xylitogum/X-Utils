using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class RendererCrossFade : MonoBehaviour
{

	public new Renderer renderer;
	public Gradient colorOverLifeTime;
	public AnimationCurve sizeOverLifeTime = AnimationCurve.Constant(0f, 1f, 1f);
	public float duration = 2f;
	public bool reversed = false;
	public bool active = false;
	public bool disableAfterFinished = false;
	public UnityEvent onFadeEndEvent;
	
	private Color[] _initialColors;
	private Vector3 _initialSize;
	private float[] _initialRenderingModes;
	private float lifetime = 0f;


	public bool debugMode = false;
	// Use this for initialization
	void Awake ()
	{
		if (!renderer)
		{
			renderer = GetComponent<Renderer>();
		}
		InitTarget();
		
	}

	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update () {

		if (active)
		{
			lifetime += Time.deltaTime;
			float ratio = duration > 0f ? lifetime / duration : 1f;
			UpdateRender(Mathf.Clamp01(reversed? 1f - ratio : ratio));
			if (lifetime >= duration) // fade finished
			{
				OnFadeEnd();
			}
		}

		
	}


	void UpdateRender(float ratio)
	{
		
		for (int i = 0; i < this.renderer.materials.Length; i++)
		{
			Material mat = this.renderer.materials[i];
			Color newColor = _initialColors[i] * colorOverLifeTime.Evaluate(ratio);
			if (debugMode)
			{
				Debug.Log(newColor);
			}
			mat.SetColor("_Color", newColor);
			this.renderer.transform.localScale = _initialSize * sizeOverLifeTime.Evaluate(ratio);
		}
		
	}
	
	public void SetTarget(Renderer targetRenderer)
	{
		if (renderer != null)
		{
			this.renderer = targetRenderer;
		}

		InitTarget();

	}

	private void OnFadeStart()
	{
		this.active = true;
		this.renderer.enabled = true;
		this.lifetime = 0f;
		for (int i = 0; i < this.renderer.materials.Length; i++)
		{
			Material mat = this.renderer.materials[i];
			mat.ChangeRenderMode(2f); //and depending on the number it outputs (0, 1, 2, 3) it maps to Opaque, Cutout, fade, transparent
		}
		
	}

	private void OnFadeEnd()
	{
		this.active = false;
		this.renderer.enabled = true;
		this.lifetime = 0f;
		if (disableAfterFinished)
		{
			this.renderer.enabled = false;
			
		}
		onFadeEndEvent.Invoke();
		for (int i = 0; i < this.renderer.materials.Length; i++)
		{
			Material mat = this.renderer.materials[i];
			mat.ChangeRenderMode(_initialRenderingModes[i]);
			mat.color = _initialColors[i];
		}
		
		renderer.transform.localScale = _initialSize;
	}
	
	private void InitTarget()
	{

		int length = this.renderer.sharedMaterials.Length;
		_initialColors = new Color[length];
		_initialRenderingModes = new float[length];
		
		for (int i = 0; i < length; i++)
		{
			Material mat = this.renderer.sharedMaterials[i];
			_initialColors[i] = mat.color;
			
			_initialRenderingModes[i] = mat.GetFloat("_Mode");
			
		}
		_initialSize = renderer.transform.localScale;
		
	}

	public void FadeOut(float duration, bool disableAfterFinished = false)
	{
		this.disableAfterFinished = disableAfterFinished;
		this.duration = duration;
		FadeOut();
	}
	
	public void FadeOut()
	{
		if (!active)
		{
			InitTarget();
		}
		reversed = false;
		OnFadeStart();
		UpdateRender(0f);
	}
	
	public void FadeIn(float duration, bool disableAfterFinished = false)
	{
		this.disableAfterFinished = disableAfterFinished;
		this.duration = duration;
		FadeIn();
	}
	
	public void FadeIn()
	{
		if (!active)
		{
			InitTarget();
		}
		reversed = true;
		OnFadeStart();
		UpdateRender(1f);
	}
	
}
