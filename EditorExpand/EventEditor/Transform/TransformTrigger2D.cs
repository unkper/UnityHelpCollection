using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventEditor {
    [ExecuteInEditMode]
    [RequireComponent(typeof(BoxCollider2D))]
    public class TransformTrigger2D : m_Trigger
    {
        private Collider2D m_trigger;
        public LayerMask checkLayer;

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
            Debug.Log("triggers2D"+this.name);
            Send(this,collider);  
        }
    }
}


