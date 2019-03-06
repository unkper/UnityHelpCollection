using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringSys
{
    public class SteeringForQueue : Steering
    {
        public float MAX_QUEUE_AHEAD;
        public float MAX_QUEUE_RADIUS;
        private Collider[] colliders;
        public LayerMask layersChecked;
        private Vehicle m_vehicle;


        void Start()
        {
            m_vehicle = GetComponent<Vehicle>();

        }

        public override Vector3 Force()
        {
            Vector3 velocity = m_vehicle.velocity;
            Vector3 normalizedVelocity = velocity.normalized;
            Vector3 ahead = transform.position + normalizedVelocity * MAX_QUEUE_AHEAD;
            colliders = Physics.OverlapSphere(ahead, MAX_QUEUE_RADIUS, layersChecked);
            if(colliders.Length>0)
            {
                foreach(Collider c in colliders)
                {
                    if((c.gameObject!=this.gameObject)&&(c.gameObject.GetComponent<Vehicle>().velocity.magnitude < velocity.magnitude))
                    {
                        m_vehicle.velocity *= 0.8f;
                        break;
                    }
                }
            }
            return new Vector3(0, 0, 0);
        }
    }
}
