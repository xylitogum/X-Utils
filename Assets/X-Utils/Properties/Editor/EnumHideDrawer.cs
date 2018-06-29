using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(EnumHideAttribute))]
class EnumHideDrawer : PropertyDrawer
{

    EnumHideAttribute enumHideAttribute
    {
        get { return ((EnumHideAttribute)attribute); }
    }
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        
        
        //get the attribute data
        
        //check if the propery we want to draw should be enabled
        bool enabled = GetEnumHideAttributeResult(property);

        //Enable/disable the property
        bool wasEnabled = GUI.enabled;
        GUI.enabled = enabled;


        //Check if we should draw the property
        if (!enumHideAttribute.HideInInspector || enabled)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }

        //Ensure that the next property that is being drawn uses the correct settings
        GUI.enabled = wasEnabled;
        
    }


    private bool GetEnumHideAttributeResult(SerializedProperty property)
    {
        
        bool enabled = false;
        
        string[] variableName = property.propertyPath.Split('.');
        if (variableName.Length <= 0) Debug.LogError("Property Path of EnumHide Attribute is not valid.");
        SerializedObject sourceObject = property.serializedObject;
        SerializedProperty sourceProperty = sourceObject.FindProperty(variableName[0]);
        string sourcePropertyPath = sourceProperty.propertyPath; 
        
        string enumPath = sourcePropertyPath.Replace(sourceProperty.name, enumHideAttribute.EnumSourceField);
        SerializedProperty resultProperty = sourceObject.FindProperty(enumPath);
       
        
        if (variableName.Length > 1 && variableName[1] == "Array") // Property is an element insied an array
        {
            // WARNING: EnumHide is not fully supported on Array objects.
            
        }
        
        
        if (resultProperty != null)
        {
            int resultPropertyValue = resultProperty.enumValueIndex;
            if (enumHideAttribute.EnumOptionName != "")
            {
                if (resultProperty.enumNames[resultPropertyValue] == enumHideAttribute.EnumOptionName)
                {
                    enabled = true;
                }
            
            }
            else if (resultPropertyValue == enumHideAttribute.EnumOption)
            {
                enabled = true;
            }
            
           
        }
        else
        {
            Debug.LogWarning("Attempting to use a EnumHideAttribute but no matching SourcePropertyValue found in object: " + enumHideAttribute.EnumSourceField);
        }
        


        return enabled;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        
        bool enabled = GetEnumHideAttributeResult(property);

        if (!enumHideAttribute.HideInInspector || enabled)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }
        else
        {
            //The property is not being drawn
            //We want to undo the spacing added before and after the property
            return -EditorGUIUtility.standardVerticalSpacing;
        }
    }


}