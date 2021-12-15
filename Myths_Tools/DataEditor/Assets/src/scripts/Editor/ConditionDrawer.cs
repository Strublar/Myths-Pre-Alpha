using Myths_Library;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomPropertyDrawer(typeof(Condition))]
public class ConditionDrawer : PropertyDrawer
{
    private int height = 0;
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return (EditorGUIUtility.singleLineHeight+2) * height+1;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUIUtility.labelWidth = 100;
        height = 0;
        EditorGUI.BeginProperty(position, label, property);
        float baseHeight = position.height;

        SerializedProperty type = property.FindPropertyRelative("conditionType");

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        DrawProperty(position, property, 0, "conditionType");
        DrawProperty(position, property, 1, "selector");
        switch (type.intValue)
        {
            case (int)ConditionType.hasStat:
                DrawProperty(position, property, 2, "inverse");
                DrawProperty(position, property, 3, "stat");
                DrawProperty(position, property, 4, "value");
                break;
            case (int)ConditionType.isEffectHolder:
                DrawProperty(position, property, 2, "inverse");
                break;
            case (int)ConditionType.isTarget:
                DrawProperty(position, property, 2, "inverse");
                break;
            case (int)ConditionType.isSource:
                DrawProperty(position, property, 2, "inverse");
                break;
            default:
                break;
                
        }


        EditorGUI.EndProperty();
    }

    private void DrawProperty(Rect position, SerializedProperty baseProperty, int index, string propertyName)
    {
        
        Rect propertyRect = new Rect(position.x, position.y + index * EditorGUIUtility.singleLineHeight + 2, 500, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(propertyRect, baseProperty.FindPropertyRelative(propertyName), new GUIContent(propertyName));
        height++;
    }

}
