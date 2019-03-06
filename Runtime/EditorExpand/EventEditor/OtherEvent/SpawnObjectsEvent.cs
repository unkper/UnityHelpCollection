using System;
using System.Collections;
using UnityEngine;
using Tools;

namespace EventEditor
{
    public class SpawnObjectsEvent:m_Event
    {
        [Serializable]
        public class Element:IComparable<Element>
        {
            public GameObject spawn;
            public Transform SpawnPoint;
            public float start = 0.0f;

            public int CompareTo(Element other)
            {
                return start >= other.start ? 1 : -1;
            }
        }

        public Element[] elements = new Element[2];
        private PriorityQueue<Element> queue;
        private float timer = 0.0f;

        void Start()
        {
            queue = new PriorityQueue<Element>(elements.Length);
            foreach(var v in elements)
            {
                queue.Push(v);
            }
        }

        public override void PlayEvents(m_Trigger sender, object info)
        {
            timer = 0.0f;
            StartCoroutine(Spawn());
        }

        IEnumerator Spawn()
        {
            while(queue.Count>=0)
            {
                if(timer>queue.Head.start)
                {
                    var pop = queue.Pop();
                    Instantiate(pop.spawn, pop.SpawnPoint.position, Quaternion.identity);
                }
                timer += Time.deltaTime;
                yield return null;
            }
        }
        
    }
}
