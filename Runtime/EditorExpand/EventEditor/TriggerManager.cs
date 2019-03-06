using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventEditor
{
    public delegate void TriggerDelegate(m_Trigger sender,object info); 
    public class TriggerManager:MonoBehaviour
    { 
        void Start()
        {
            //初始化字典
            Debug.Log(this.name);
            var parent = GameObject.Find("EventAndTrigger");
            Transform t = parent.transform.Find("Triggers");
            Transform e = parent.transform.Find("Events");
            for(int i=0;i<t.childCount;i++)
            {
                var key = t.GetChild(i).gameObject.GetComponent<m_Trigger>();
                var value = e.GetChild(i).gameObject.GetComponents<m_Event>();
                foreach (var v in value)
                    key.trigger += new TriggerDelegate(v.PlayEvents);
            }
        }
    }

    public abstract class m_Trigger:MonoBehaviour
    {
        public event TriggerDelegate trigger;

        protected void Send(m_Trigger sender,object info)
        {
            if (trigger != null)
                trigger(sender, info);
        }

        protected void Send(m_Trigger sender,object info,float delayTime)
        {
            StartCoroutine(DelaySend(sender, info, delayTime));
        }

        IEnumerator DelaySend(m_Trigger sender, object info, float delayTime)
        {
            while(delayTime>0)
            {
                delayTime -= Time.deltaTime;
                yield return null;
            }
            Send(sender, info);
            yield break;
        }
    }

    public abstract class m_Event : MonoBehaviour
    {
        public abstract void PlayEvents(m_Trigger sender,object info);
    }

}
