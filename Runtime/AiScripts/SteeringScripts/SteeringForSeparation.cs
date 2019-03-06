using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SteeringSys
{
    public class SteeringForSeparation : Steering
    {
        public float comfortDistence = 1.0f;
        public float multiplyInsideComfortDistance = 2;

        void Start()
        {
        }

        public override Vector3 Force()
        {
            Vector3 SteeringForce = new Vector3(0,0,0);
            foreach(GameObject s in GetComponent<Radar>().neighbors)
            {
                if(s!=null&&s!=this.gameObject)
                {
                    Vector3 toNeighbor = transform.position - s.transform.position;
                    float length = toNeighbor.magnitude;
                    SteeringForce += toNeighbor.normalized / length;
                    if(length<comfortDistence)
                    {
                        SteeringForce *= multiplyInsideComfortDistance;
                    }
                }
            }
            return SteeringForce;
        }

    }
}
