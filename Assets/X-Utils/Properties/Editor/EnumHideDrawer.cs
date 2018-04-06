using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(EnumHideAttribute))]
class EnumHideDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //get the attribute data
        EnumHideAttribute enumHAtt = (EnumHideAttribute)attribute;
        //check if the propery we want to draw should be enabled
        bool enabled = GetEnumHideAttributeResult(enumHAtt, property);

        //Enable/disable the property
        bool wasEnabled = GUI.enabled;
        GUI.enabled = enabled;


        //Check if we should draw the property
        if (!enumHAtt.HideInInspector || enabled)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }

        //Ensure that the next property that is being drawn uses the correct settings
        GUI.enabled = wasEnabled;
    }


    private bool GetEnumHideAttributeResult(EnumHideAttribute enumHAtt, SerializedProperty property)
    {
        bool enabled = false;
        //Look for the sourcefield within the object that the property belongs to
        string propertyPath = property.propertyPath; //returns the property path of the property we want to apply the attribute to
        string enumPath = propertyPath.Replace(property.name, enumHAtt.EnumSourceField); //changes the path to the enumsource property path
        int sourcePropertyValue = property.serializedObject.FindProperty(enumPath).enumValueIndex;
        if (enumHAtt.EnumOptionName != "")
        {
            if (property.serializedObject.FindProperty(enumPath).enumNames[sourcePropertyValue] == enumHAtt.EnumOptionName)
            {
                enabled = true;
            }
            
        }
        else if (sourcePropertyValue == enumHAtt.EnumOption)
        {
            enabled = true;
        }


        return enabled;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        EnumHideAttribute enumHAtt = (EnumHideAttribute)attribute;
        bool enabled = GetEnumHideAttributeResult(enumHAtt, property);

        if (!enumHAtt.HideInInspector || enabled)
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
