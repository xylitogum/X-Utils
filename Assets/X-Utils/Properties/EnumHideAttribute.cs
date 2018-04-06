using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
    AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class EnumHideAttribute : PropertyAttribute
{
    //The name of the bool field that will be in control
    public string EnumSourceField = "";

    //The name of the bool field that will be in control
    public int EnumOption = 0;


    //TRUE = Hide in inspector / FALSE = Disable in inspector 
    public bool HideInInspector = false;

    public EnumHideAttribute(string enumSourceField, int enumOption)
    {
        this.EnumSourceField = enumSourceField;
        this.EnumOption = enumOption;
        this.HideInInspector = false;
    }

    public EnumHideAttribute(string enumSourceField, int enumOption, bool hideInInspector)
    {
        this.EnumSourceField = enumSourceField;
        this.EnumOption = enumOption;
        this.HideInInspector = hideInInspector;
    }
}
