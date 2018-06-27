using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
class MinMaxSliderDrawer : PropertyDrawer
{
    //private const float FIELD_WIDTH = 40f;
    private const float LABEL_WIDTH = 35f;
    private const float LINE_SPACE = 5f;
    private const float FIELD_SPACE = 5f;
    private const string LABEL_MIN = "Min:";
    private const string LABEL_MAX = "Max:";

    private int _numberOfLines = 2;
    private Rect _rectPosition;
    
    float _previousLabelWidth;
    //float _previousFieldWidth;
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        _rectPosition = position;

        if (property.propertyType != SerializedPropertyType.Vector2)
        {
            EditorGUI.LabelField(position, label, "MinMaxSlider only works with Vector2");
            return;
        }

        Vector2 range = property.vector2Value;
        float min = range.x;
        float max = range.y;
        MinMaxSliderAttribute attr = attribute as MinMaxSliderAttribute;

        // Draw MinMax Slider
        EditorGUI.BeginChangeCheck();
        Rect rect = new Rect(position.x, position.y, position.width, GetHeight());
        EditorGUI.MinMaxSlider(rect, label, ref min, ref max, attr.min, attr.max);
        if (EditorGUI.EndChangeCheck())
        {
            range.x = min;
            range.y = max;
            property.vector2Value = range;
        }


        // Draw Float/Int Field
        
        _previousLabelWidth = EditorGUIUtility.labelWidth;
        //_previousFieldWidth = EditorGUIUtility.fieldWidth;
        EditorGUIUtility.labelWidth = LABEL_WIDTH;
        //EditorGUIUtility.fieldWidth = FIELD_WIDTH;
        
        EditorGUI.BeginChangeCheck();

        if (attr.hasInt)
        {
            int intMin = EditorGUI.IntField(
                new Rect(GetFieldX(0), GetFieldY(1), GetFieldWidth(), GetHeight()),
                new GUIContent(LABEL_MIN), (int)min);
            int intMax = EditorGUI.IntField(
                new Rect(GetFieldX(1), GetFieldY(1), GetFieldWidth(), GetHeight()),
                new GUIContent(LABEL_MAX), (int)max);
            if (EditorGUI.EndChangeCheck())
            {
                range.x = Mathf.Clamp(intMin, attr.min, attr.max);
                range.y = Mathf.Clamp(intMax, attr.min, attr.max);
                property.vector2Value = range;
            
            }
        }
        else
        {
            min = EditorGUI.FloatField(
                new Rect(GetFieldX(0), GetFieldY(1), GetFieldWidth(), GetHeight()),
                new GUIContent(LABEL_MIN), min);
            max = EditorGUI.FloatField(
                new Rect(GetFieldX(1), GetFieldY(1), GetFieldWidth(), GetHeight()),
                new GUIContent(LABEL_MAX), max);
            if (EditorGUI.EndChangeCheck())
            {
                range.x = Mathf.Clamp(min, attr.min, attr.max);
                range.y = Mathf.Clamp(max, attr.min, attr.max);
                property.vector2Value = range;
            
            }
        }

        
        //EditorGUI.EndProperty();
        EditorGUIUtility.labelWidth = _previousLabelWidth;
        //EditorGUIUtility.fieldWidth = _previousFieldWidth;

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return _numberOfLines*(EditorGUI.GetPropertyHeight(property, label) + LINE_SPACE);
    }

    #region GUI_SIZE
    
    float GetFieldWidth()
    {
        return (_rectPosition.width - _previousLabelWidth) / 2f - FIELD_SPACE;
    }
    
    float GetHeight()
    {
        return _rectPosition.height /(float)_numberOfLines - LINE_SPACE;
    }

    float GetFieldX(int id)
    {
        return _rectPosition.x + _previousLabelWidth + (GetFieldWidth() + FIELD_SPACE) * id;
    }
    
    float GetFieldY(int id)
    {
        return _rectPosition.y + GetHeight() * id;
    }
    
    #endregion


}
