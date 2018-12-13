using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class TextureExtension
{
	public static Texture2D toTexture2D(this RenderTexture rTex)
	{
		RenderTexture currentActiveRT = RenderTexture.active;
		Texture2D tex = new Texture2D(512, 512, TextureFormat.RGB24, false);
		RenderTexture.active = rTex;
		tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
		tex.Apply();
		RenderTexture.active = currentActiveRT;

		return tex;
	}
}