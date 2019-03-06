using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EventEditor {
    public class EventEditorWindow : EditorWindow
    {
        static int nowIndex = 0;
        static GameObject manager;
        static List<GameObject> triggers = new List<GameObject>(4);
        static List<GameObject> events = new List<GameObject>(4);
        static List<int> freeSlots = new List<int>();
        static GameObject triggersParent;
        static GameObject eventsParent;
        static int removeIndex = 0;


        [MenuItem("编辑器扩展/事件编辑器")]
        static public void OpenWindow()
        {
            Init();
            FindAll();
            var w = GetWindow<EventEditorWindow>();
            w.Show();
        }

        void OnDisable()
        {
            nowIndex = 0;
        }

        static void FindAll()
        {
            nowIndex = 1;
            var Tparent = triggersParent.transform;
            var Eparent = eventsParent.transform;
            //遍历获取所有的trigger和event，一旦有一个没有挂接上脚本就将两个都销毁
            for(int i = 1;i<Tparent.childCount;i++)
            {
                try
                {
                    var Tchild = Tparent.GetChild(i);
                    var Echild = Eparent.GetChild(i);
                    nowIndex++;
                    if(Tchild.GetComponent<m_Trigger>()==null||Echild.GetComponent<m_Event>()==null)
                    {
                        DestroyImmediate(Tchild.gameObject);
                        DestroyImmediate(Echild.gameObject);
                        freeSlots.Add(Tchild.name[-1]);
                    }
                    else
                    {
                        triggers.Add(Tchild.gameObject);
                        events.Add(Echild.gameObject);
                    }
                }
                catch(NullReferenceException e)
                {
                    Debug.LogError("找不到对应的child");
                }
            }
        }

        static void Init()
        {
            TriggerManager triggerManager;
            if ((manager = GameObject.Find("EventManager")) == null)
            {
                manager = new GameObject("EventManager");
                triggerManager = manager.AddComponent<TriggerManager>();
            }
            else
                triggerManager = manager.GetComponent<TriggerManager>();
            if (GameObject.Find("EventAndTrigger") == null)
            {
                var g = new GameObject("EventAndTrigger");
                eventsParent = new GameObject("Events");
                triggersParent = new GameObject("Triggers");
                eventsParent.transform.parent = g.transform;
                triggersParent.transform.parent = g.transform;
            }
            else
            {
                var g = GameObject.Find("EventAndTrigger");
                eventsParent = g.transform.Find("Events").gameObject;
                triggersParent = g.transform.Find("Triggers").gameObject;
            }
        }

        void OnGUI()
        {
            /*如果添加事件按下，创建两个gameobject分别对应
             * trigger与event，在这两个gameobject上绑定m_trigger与m_event脚本
             * 管理器在游戏开始时会获取所有的脚本并添加到字典
             */
            GUILayout.Label("这个事件编辑器与event和trigger脚本使用，\n" +
                "点击按钮后会在游戏场景中添加两个gameobject--event与trigger\n" +
                "他们的编号是相同的，你要做的就是在这两个gameobject上绑定脚本\n" +
                "作者：杨清杨");
            if (GUILayout.Button("添加事件"))
            {
                if(freeSlots.Count>0)
                {
                    var v1 = new GameObject("Event" + freeSlots[0]);
                    var v2 = new GameObject("Trigger" + freeSlots[0]);
                    v2.transform.parent = triggersParent.transform;
                    v1.transform.parent = eventsParent.transform;
                    freeSlots.RemoveAt(0);
                }
                else
                {
                    var v1 = new GameObject("Event" + nowIndex);
                    var v2 = new GameObject("Trigger" + nowIndex);
                    v2.transform.parent = triggersParent.transform;
                    v1.transform.parent = eventsParent.transform;
                    nowIndex++;
                }
            }
            if(GUILayout.Button("删除事件")&&freeSlots.IndexOf(removeIndex)==-1)
            {
                DestroyImmediate(triggers[removeIndex].gameObject);
                DestroyImmediate(events[removeIndex].gameObject);
                triggers.RemoveAt(removeIndex);
                events.RemoveAt(removeIndex);
                freeSlots.Add(removeIndex);
                Debug.Log(triggers.Count + "   " + events.Count);
            }
        }

    }
}


