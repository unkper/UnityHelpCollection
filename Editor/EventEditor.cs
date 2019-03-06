using System;
using Tools;
using UnityEditor;
using UnityEngine;
using EventEditor;

[CustomEditor(typeof(TransformEvent))]
public class TransformEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var show = target as TransformEvent;
        show.ifEnableObject = GUILayout.Toggle(show.ifEnableObject,"是否为禁用物体");
        if(show.ifEnableObject)
        {
            show.enableIt = GUILayout.Toggle(show.enableIt, "禁用还是启用");
            show.enableObject = EditorGUILayout.ObjectField(show.enableObject, typeof(GameObject), true) as GameObject;
        }
        else
            show.mover = EditorGUILayout.ObjectField(show.mover, typeof(PositionMover), true) as PositionMover;
    }
}

