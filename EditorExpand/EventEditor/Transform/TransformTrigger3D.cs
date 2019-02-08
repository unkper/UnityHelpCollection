using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventEditor
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(BoxCollider))]
    public class TransformTrigger3D : m_Trigger
    {
        private Collider m_trigger;
        public string checkTag;

        // Use this for initialization
        void Start()
        {
            if ((m_trigger = this.GetComponent<Collider>()) == null)
                m_trigger = gameObject.AddComponent<BoxCollider>();
            m_trigger.isTrigger = true;
        }

        void OnDrawGizmos()
        {
            if (m_trigger == null)
                return;
            else
            {
                Bounds bound;
                bound = m_trigger.bounds;
                Gizmos.color = new Color(1, 0, 0, 0.3f);
                Gizmos.DrawCube(bound.center, bound.size);
            }
        }

        void OnTriggerEnter(Collider collider)
        {
            Debug.Log("triggers" + this.name);
            if(collider.tag==checkTag)
                Send(this, collider);
        }
    }
}
