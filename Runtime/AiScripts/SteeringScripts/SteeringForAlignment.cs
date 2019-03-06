using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringSys
{
    public class SteeringForAlignment : Steering
    {
        private float maxSpeed;
        void Start()
        {
            maxSpeed = GetComponent<Vehicle>().maxSpeed;
        }
        public override Vector3 Force()
        {
            Vector3 averageDirection = new Vector3(0, 0, 0);
            int neighborCount = 0;
            foreach(GameObject s in GetComponent<Radar>().neighbors)
            {
                if((s!=null)&&(s!=this.gameObject))
                {
                    averageDirection += s.transform.forward;
                    neighborCount++;
                }
            }
            if(neighborCount>0)
            {
                averageDirection /= (float)neighborCount;
                averageDirection -= transform.forward;
            }
            averageDirection = averageDirection.normalized* maxSpeed;
            return averageDirection;
        }

    }
}
