using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIscripts
{
    public class SteeringForArrive:Steering,IArriveSteering,ITargetSteering
    {
        public bool isPlanar = true;
        public float arrivalDistance = 0.3f;
        public float characterRadius = 1.2f;
        public float slowDownDistance;
        public GameObject target;
        private bool arrive = false;
        public event ArriveEventHandler Arrive;
        private Vector3 desiredVelocity;
        private Vehicle m_vehicle;
        private float maxSpeed;
        void Start()
        {
            m_vehicle = GetComponent<Vehicle>();
            slowDownDistance = m_vehicle.SlowDownDis;
            arrivalDistance = m_vehicle.StoppingDis;
            characterRadius = m_vehicle.characterRadius;
            maxSpeed = m_vehicle.maxSpeed;
            isPlanar = m_vehicle.isPlanar;
        }
        public override Vector3 Force()
        {
            Vector3 toTarget = target.transform.position - transform.position;
            Vector3 desiredVelocity;
            Vector3 returnForce;
            if (isPlanar)
                toTarget.y = 0;
            float distance = toTarget.magnitude;
            if (distance < arrivalDistance && Arrive != null)
            {
                Arrive();
                arrive = true;
            }
                
            if(distance>slowDownDistance)
            {
                desiredVelocity = toTarget.normalized * maxSpeed;
                returnForce = desiredVelocity - m_vehicle.velocity;
            }
            else
            {
                desiredVelocity = toTarget - m_vehicle.velocity;
                returnForce = desiredVelocity - m_vehicle.velocity;
            }
            return returnForce;
        }

        public bool hasArrive()
        {
            return arrive;
        }

        public Transform GetTarget()
        {
            return target.transform;
        }

        public void SetTarget(Transform target)
        {
            arrive = false;
            this.target = target.gameObject;
        }
    }
}
