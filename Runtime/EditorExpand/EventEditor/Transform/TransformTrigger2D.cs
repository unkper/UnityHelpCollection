using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventEditor {
    using UnityEngine.Events;
    [ExecuteInEditMode]
    [RequireComponent(typeof(BoxCollider2D))]
    public class TransformTrigger2D : m_Trigger
    {
        private Collider2D m_trigger;
        public LayerMask checkLayer;
        public event UnityAction<Collider2D> enter;
        public event UnityAction<Collider2D> stay;
        public event UnityAction<Collider2D> exit;

        // Use this for initialization
        void Start()
        {
            if ((m_trigger = this.GetComponent<Collider2D>()) == null)
                m_trigger = gameObject.AddComponent<BoxCollider2D>();
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

        void OnTriggerEnter2D(Collider2D collider)
        {
            enter(collider);
            Send(this,collider);  
        }

        void OnTriggerStay2D(Collider2D collider)
        {
            stay(collider);
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            exit(collider);
        }

    }
}


