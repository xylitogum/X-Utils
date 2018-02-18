using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
class MinMaxSliderDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        if (property.propertyType == SerializedPropertyType.Vector2)
        {
            Vector2 range = property.vector2Value;
            float min = range.x;
            float max = range.y;
            MinMaxSliderAttribute attr = attribute as MinMaxSliderAttribute;

            // Draw MinMax Slider
            EditorGUI.BeginChangeCheck();
            Rect rect = new Rect(position.x, position.y, position.width, position.height / 2f);
            EditorGUI.MinMaxSlider(position, label, ref min, ref max, attr.min, attr.max);
            if (EditorGUI.EndChangeCheck())
            {
                range.x = min;
                range.y = max;
                property.vector2Value = range;
            }


            // Draw Float/Int Field
            EditorGUI.BeginChangeCheck();

            GUIContent[] sub_labels = new GUIContent[] { new GUIContent(">"), new GUIContent("<") };
            Rect sub_rect = new Rect(position.x, position.y + position.height / 2f, position.width, position.height / 2f);

            if (attr.hasInt) {
                int[] sub_ranges = new int[] { (int)min, (int)max };
                EditorGUI.MultiIntField(sub_rect, sub_labels, sub_ranges);
                if (EditorGUI.EndChangeCheck())
                {
                    range.x = (int)Mathf.Clamp(sub_ranges[0], attr.min, attr.max);
                    range.y = (int)Mathf.Clamp(sub_ranges[1], attr.min, attr.max);
                    property.vector2Value = range;
                }
            }
            else {
                float[] sub_ranges = new float[] { min, max };
                EditorGUI.MultiFloatField(sub_rect, sub_labels, sub_ranges);
                if (EditorGUI.EndChangeCheck())
                {
                    range.x = Mathf.Clamp(sub_ranges[0], attr.min, attr.max);
                    range.y = Mathf.Clamp(sub_ranges[1], attr.min, attr.max);
                    property.vector2Value = range;
                }
            }


           
            //EditorGUI.EndProperty();
        }
        else
        {
            EditorGUI.LabelField(position, label, "Use only with Vector2");
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return 42f;
    }


}