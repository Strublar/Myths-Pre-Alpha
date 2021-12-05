using Myths_Library;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Effect))]
public class EffectDrawer : PropertyDrawer
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

        SerializedProperty type = property.FindPropertyRelative("effectType");

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        DrawProperty(position, property, 0, "effectType");
        switch (type.intValue)
        {
            case (int)EffectType.gainMastery:
                DrawProperty(position, property, 1, "element");
                DrawProperty(position, property, 2, "amount");
                break;
            case (int)EffectType.dealDamage:
                DrawProperty(position, property, 1, "amount");
                
                break;
            case (int)EffectType.modifyStat:
                DrawProperty(position, property, 1, "stat");
                DrawProperty(position, property, 2, "amount");
                DrawProperty(position, property, 3, "temporary");
                break;
            case (int)EffectType.reduceSpellCost:
                DrawProperty(position, property, 1, "element");
                DrawProperty(position, property, 2, "amount");
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
