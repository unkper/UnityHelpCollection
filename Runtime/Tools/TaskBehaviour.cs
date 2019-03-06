using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Tools
{
    public class WaitTimeManager
    {
        class TaskBehaviour : MonoBehaviour
        { }

        private static TaskBehaviour m_Task;
        static WaitTimeManager()
        {
            GameObject go = new GameObject("#WaitTimeManager");
            GameObject.DontDestroyOnLoad(go);
            m_Task = go.AddComponent<TaskBehaviour>();
        }

        static public Coroutine WaitTime(float time,UnityAction calllback)
        {
            return m_Task.StartCoroutine(mCoroutine(time, calllback));
        }

        static public void CancelWait(ref Coroutine coroutine)
        {
            if (coroutine != null)
            {
                m_Task.StopCoroutine(coroutine);
                coroutine = null;
            }
        }

        static IEnumerator mCoroutine(float time,UnityAction callback)
        {
            yield return new WaitForSeconds(time);
            if (callback != null) callback();
        }

        static IEnumerator mTimeCallBack(int count,float delta,UnityAction callback)
        {
            for(int i = 0; i < count; i++)
            {
                yield return new WaitForSeconds(delta);
                callback();
            }
        }
    }
}
