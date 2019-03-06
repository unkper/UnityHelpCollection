using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringSys
{
    public class Radar : MonoBehaviour
    {
        private Collider[] colliders;
        private float timer = 0;
        public List<GameObject> neighbors;
        public float checkInterval = 0.3f;
        public float detactRadius = 10f;
        public LayerMask layersChecked;

        void Start()
        {
            neighbors = new List<GameObject>();
        }

        void Update()
        {
            timer += Time.deltaTime;
            if(timer>checkInterval)
            {
                neighbors.Clear();
                colliders = Physics.OverlapSphere(transform.position, detactRadius, layersChecked);
                for(int i=0;i<colliders.Length;i++)
                {
                    if(colliders[i].GetComponent<Vehicle>())
                    {
                        neighbors.Add(colliders[i].gameObject);
                    }
                }
                timer = 0;
            }
        }
    }
}
