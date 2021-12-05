using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Myths_Library;

//[CustomPropertyDrawer(typeof(Trigger))]
public class TriggerDrawer : PropertyDrawer
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

        SerializedProperty type = property.FindPropertyRelative("triggerType");

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        DrawProperty(position, property, 0, "selector");
        DrawProperty(position, property, 1, "eventType");




        EditorGUI.EndProperty();
    }

    private void DrawProperty(Rect position, SerializedProperty baseProperty, int index, string propertyName)
    {
        
        Rect propertyRect = new Rect(position.x, position.y + index * EditorGUIUtility.singleLineHeight + 2, 500, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(propertyRect, baseProperty.FindPropertyRelative(propertyName), new GUIContent(propertyName));
        height++;
    }

}
