using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventEditor
{
    using UnityEngine.Events;
    [ExecuteInEditMode]
    [RequireComponent(typeof(BoxCollider))]
    public class TransformTrigger3D : m_Trigger
    {
        private Collider m_trigger;
        public string checkTag;
        public event UnityAction<Collider> enter;
        public event UnityAction<Collider> stay;
        public event UnityAction<Collider> exit;

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
            //Debug.Log("triggers" + this.name);
            enter(collider);
            if(collider.tag==checkTag)
                Send(this, collider);
        }

        void OnTriggerStay(Collider collider)
        {
            stay(collider);
        }

        void OnTriggerExit(Collider collider)
        {
            exit(collider);
        }

    }
}
