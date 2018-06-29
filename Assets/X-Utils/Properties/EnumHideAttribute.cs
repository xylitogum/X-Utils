using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
                AttributeTargets.Struct, Inherited = true, AllowMultiple = true)]
public class EnumHideAttribute : PropertyAttribute
{
	//The name of the bool field that will be in control
	public string EnumSourceField = "";

	//The name of the bool field that will be in control
	public int EnumOption = 0;
	public string EnumOptionName = "";


	//TRUE = Hide in inspector / FALSE = Disable in inspector 
	public bool HideInInspector = false;
    
	public EnumHideAttribute(string enumSourceField, int enumOption, bool hideInInspector=false)
	{
		this.EnumSourceField = enumSourceField;
		this.EnumOption = enumOption;
		this.HideInInspector = hideInInspector;
	}
    

	public EnumHideAttribute(string enumSourceField, string enumOptionName, bool hideInInspector=false)
	{
		this.EnumSourceField = enumSourceField;
		this.EnumOptionName = enumOptionName;
		this.HideInInspector = hideInInspector;
	}
}