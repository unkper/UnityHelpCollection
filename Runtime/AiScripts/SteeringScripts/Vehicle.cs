using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SteeringSys
{
    [RequireComponent(typeof(Radar))]
    public class Vehicle : MonoBehaviour
    {
        private Steering[] steerings;
        public float maxSpeed = 10;
        public float maxForce = 100;
        protected float sqrMaxSpeed;
        public float mass = 1;
        public Vector3 velocity;
        public float damping = 0.9f;
        public float computeInterval = 0.2f;
        public bool isPlanar = true;
        private Vector3 steeringForce;
        protected Vector3 acceleration;
        public float timer;

        /// <summary>
        /// 角色在何距离时开始减速 
        /// </summary>
        public float SlowDownDis;
        /// <summary>
        /// 角色的大小
        /// </summary>
        public float characterRadius;
        /// <summary>
        /// 角色距离物体多近时停止
        /// </summary>
        public float StoppingDis;

        public void Stop()
        {
            acceleration = Vector3.zero;
            velocity = Vector3.zero;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, StoppingDis);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, SlowDownDis);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, characterRadius);
        }

        void Awake()
        {
            steeringForce = new Vector3(0, 0, 0);
            sqrMaxSpeed = maxSpeed * maxSpeed;
            timer = 0;
            
            steerings = GetComponents<Steering>();
            foreach (var s in steerings)
            {
                s.enabled = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            steeringForce = new Vector3(0, 0, 0);
            if(timer>computeInterval)
            {
                foreach(Steering s in steerings)
                {
                    if (s.enabled)
                    {
                        steeringForce += s.Force() * s.weight;
                    }
                        
                }
                steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);
                acceleration = steeringForce / mass;
                timer = 0;
            } 
        }
    }

}


