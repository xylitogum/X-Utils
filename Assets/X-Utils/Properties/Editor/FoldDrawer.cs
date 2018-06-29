using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(FoldAttribute))]
class FoldDrawer : PropertyDrawer
{
    public bool foldout = false;
    FoldAttribute foldAttribute
    {
        get { return ((FoldAttribute)attribute); }
    }


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty propertyIterator = property;
        
        position.height = EditorGUIUtility.singleLineHeight;
        
        foldout = EditorGUI.Foldout(
            new Rect(position.x, position.y, position.width, position.height),
            foldout, foldAttribute.title);
        if (foldout)
        {
            ++EditorGUI.indentLevel;
            Rect nextPosition = position;
            for (int i = 0; i < foldAttribute.numberOfItems; i++)
            {
                nextPosition.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                nextPosition.height = EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(nextPosition, propertyIterator);
                propertyIterator.Dispose();
                
                if (propertyIterator.NextVisible(true))
                {
                    
                }
                else
                {
                    break;
                }
                
            }
            --EditorGUI.indentLevel;
        }
        
    }
    
    
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return foldout? (1 + foldAttribute.numberOfItems) * (EditorGUI.GetPropertyHeight(property, label) + EditorGUIUtility.standardVerticalSpacing) : EditorGUI.GetPropertyHeight(property, label);
    }
}


[CustomPropertyDrawer(typeof(FoldedAttribute))]
class FoldedDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        //EditorGUI.PropertyField(position, property, label);
        EditorGUI.EndProperty();
    }
}
