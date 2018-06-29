using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
                AttributeTargets.Class | AttributeTargets.Struct, Inherited = true, AllowMultiple = true)]
public class DisabledAttribute : PropertyAttribute {
	
    
}
