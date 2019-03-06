using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringSys
{
    public class SteeringForAvoid : Steering
    {
        public GameObject avoidThing;
        public float avoidRadius = 1.0f;
        public int radiusAngle = 30;
        public float maxRandomTremble = 0.5f;
        public Vector3 aim;
        public bool isPlanar = true;

        private Transform toTransform;
        private Vehicle m_vehicle;
        private float maxForce;

        // Use this for initialization
        void Start()
        {
            m_vehicle = GetComponent<Vehicle>();
            maxForce = m_vehicle.maxForce;
            toTransform = avoidThing.GetComponent<Transform>();
        }

        void OnEnable()
        {
            var ctrl = GetComponent<AILocomotion>();
            ctrl.ifTurning = false;
        }

        public override Vector3 Force()
        {
            float randomAngle = (Random.value - 0.5f) * radiusAngle;
            Vector3 vector = (this.transform.position - toTransform.position).normalized;
            vector = Quaternion.Euler(0, randomAngle, 0) * vector;
            aim = toTransform.position + vector * (avoidRadius + maxRandomTremble * (Random.value - 0.5f));
            if (isPlanar)
                aim.y = 0;
            return (aim - this.transform.position).normalized * maxForce;

        }

        void OnDisable()
        {
            var ctrl = GetComponent<AILocomotion>();
            ctrl.ifTurning = true;
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawSphere(aim, 1.0f);
        }


    }
}