using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public static class Vector3Extension {
	
	
	
	public static Vector3Int Abs(this Vector3Int v)
	{
		
		return new Vector3Int(v.x >= 0?v.x:-v.x, v.y >= 0?v.y:-v.y, v.z >= 0?v.z:-v.z);
	}
	
	public static Vector3 Abs(this Vector3 v)
	{
		
		return new Vector3(v.x >= 0f?v.x:-v.x, v.y >= 0f?v.y:-v.y, v.z >= 0f?v.z:-v.z);
	}
	
	
}


public static class TransformExtension {
	
	
	/// <summary>
	/// Transforms a Ray from world space to local space
	/// </summary>
	/// <param name="transform">the local transform</param>
	/// <param name="ray">world space ray</param>
	/// <returns>local space ray</returns>
	public static Ray InverseTransformRay(this Transform transform, Ray ray)
	{
		return new Ray(transform.InverseTransformPoint(ray.origin), transform.InverseTransformDirection(ray.direction));
	}
	
	
	/// <summary>
	/// Transforms a Ray from local space to world space
	/// </summary>
	/// <param name="transform">the local transform</param>
	/// <param name="ray">local space ray</param>
	/// <returns>world space ray</returns>
	public static Ray TransformRay(this Transform transform, Ray ray)
	{
		return new Ray(transform.TransformPoint(ray.origin), transform.TransformDirection(ray.direction));
	}
	
	
	
}

public static class MathX
{
	/// <summary>
	/// Modulo of X by M. Different from remainder since it always returns positive value.
	/// </summary>
	/// <param name="x"></param>
	/// <param name="m"></param>
	/// <returns></returns>
	public static int Modulo(int x, int m) {
		int r = x%m;
		return r<0 ? r+m : r;
	}
}