using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
class MinMaxSliderDrawer : PropertyDrawer
{
    //private const float FIELD_WIDTH = 40f;
    private const float LABEL_WIDTH = 35f;
    private const float FIELD_SPACE = 5f;
    private const string LABEL_MIN = "Min:";
    private const string LABEL_MAX = "Max:";

    private int _numberOfLines = 2;
    private Rect _rectPosition;
    
    float _previousLabelWidth;
    //float _previousFieldWidth;
    
    MinMaxSliderAttribute minMaxSliderAttribute
    {
        get { return ((MinMaxSliderAttribute)attribute); }
    }
    
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        _rectPosition = position;

        if (property.propertyType != SerializedPropertyType.Vector2)
        {
            EditorGUI.LabelField(position, label, "MinMaxSlider only works with Vector2");
            return;
        }
        
        //EditorGUI.BeginProperty(position, label, property);

        Vector2 range = property.vector2Value;
        float min = range.x;
        float max = range.y;

        // Draw MinMax Slider
        EditorGUI.BeginChangeCheck();
        Rect rect = new Rect(position.x, position.y, position.width, GetHeight());
        EditorGUI.MinMaxSlider(rect, label, ref min, ref max, minMaxSliderAttribute.min, minMaxSliderAttribute.max);
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

        min = EditorGUI.FloatField(
            new Rect(GetFieldX(0), GetFieldY(1), GetFieldWidth(), GetHeight()),
            new GUIContent(LABEL_MIN), min);
        max = EditorGUI.FloatField(
            new Rect(GetFieldX(1), GetFieldY(1), GetFieldWidth(), GetHeight()),
            new GUIContent(LABEL_MAX), max);
        if (EditorGUI.EndChangeCheck())
        {
            range.x = Mathf.Clamp(min, minMaxSliderAttribute.min, minMaxSliderAttribute.max);
            range.y = Mathf.Clamp(max, minMaxSliderAttribute.min, minMaxSliderAttribute.max);
            property.vector2Value = range;
            
        }

        
        EditorGUIUtility.labelWidth = _previousLabelWidth;
        //EditorGUIUtility.fieldWidth = _previousFieldWidth;
        //EditorGUI.EndProperty();

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return _numberOfLines*(EditorGUI.GetPropertyHeight(property, label) + EditorGUIUtility.standardVerticalSpacing);
    }

    #region GUI_SIZE
    
    float GetFieldWidth()
    {
        return (_rectPosition.width - _previousLabelWidth) / 2f - FIELD_SPACE;
    }
    
    float GetHeight()
    {
        return _rectPosition.height /(float)_numberOfLines - EditorGUIUtility.standardVerticalSpacing;
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
