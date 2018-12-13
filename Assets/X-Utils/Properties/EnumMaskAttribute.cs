using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnumMaskAttribute : PropertyAttribute
{
    public string enumName;
 
    public EnumMaskAttribute() {}
 
    public EnumMaskAttribute(string name)
    {
        enumName = name;
    }
}