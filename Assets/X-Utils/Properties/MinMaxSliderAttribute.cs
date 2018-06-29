using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class MinMaxSliderAttribute : PropertyAttribute
{

    public readonly float max;
    public readonly float min;
    //public readonly bool hasInt;

    public MinMaxSliderAttribute(float min, float max)
    {
        this.min = min;
        this.max = max;
        //this.hasInt = hasInt;
    }
}