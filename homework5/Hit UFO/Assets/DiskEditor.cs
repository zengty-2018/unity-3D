using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditor(typeof(DiskData))]
[CanEditMultipleObjects]

public class DiskEditor : MonoBehaviour
{
    SerializedProperty score;
    SerializedProperty color;
    SerializedProperty scale;

    void OnEnable(){
        score = serializedObject.FindProperty("score");
        color = serializedObject.FindProperty("color");
        scale = serializedObject.FindProperty("scale");
    }

    public override void OnInspectorGUI(){
        serializedObject.Update();
        EditorGUILayout.IntSlider(score, 0, 5, new GUIContent("score"));
        EditorGUILayout.PropertyField(color);
		EditorGUILayout.PropertyField(scale);
		serializedObject.ApplyModifiedProperties();
    }
}
