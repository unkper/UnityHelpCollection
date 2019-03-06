using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonsterSpawner))]
public class MonEditor : Editor {
    int index = 0;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var target = (MonsterSpawner)serializedObject.targetObject;
        if(GUILayout.Button("添加生成点"))
        {
            var g = new GameObject("生成点"+index.ToString());
            index++;
            g.transform.parent = target.transform;
            target.spawnPoints.Add(g.transform);
        }
        if(GUILayout.Button("删除最后一个生成点"))
        {
            Debug.Log(target.spawnPoints.Count);
            target.spawnPoints.RemoveAt(target.spawnPoints.Count - 1);
        }
    }


}
