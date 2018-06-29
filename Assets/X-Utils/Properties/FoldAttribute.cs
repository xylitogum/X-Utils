using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Obsolete]
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class FoldAttribute : PropertyAttribute
{

	public readonly string title;
	public readonly int numberOfItems;

	public FoldAttribute(string title, int numberOfItems = 1)
	{
		this.title = title;
		this.numberOfItems = numberOfItems;
	}
}


[System.Obsolete]
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class FoldedAttribute : PropertyAttribute
{

}
