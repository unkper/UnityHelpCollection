using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using editTool = UnityEditor.EditorGUILayout;

namespace FinalStateMachine
{
    [CustomEditor(typeof(FSMToolSys))]
    public class FsmToolEditor:Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
          
            

        }

    }
}
