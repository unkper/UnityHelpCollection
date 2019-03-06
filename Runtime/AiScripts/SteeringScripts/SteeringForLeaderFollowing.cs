using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringSys
{
    [RequireComponent(typeof(SteeringForArrive))]
    public class SteeringForLeaderFollowing : Steering
    {
        public Vector3 target;
        private Vector3 desiredVelocity;
        private Vehicle m_vehicle;
        private float maxSpeed;
        private bool isPlanar;

        public GameObject leader;
        private Vehicle leaderController;
        private Vector3 leaderVelocity;
        public float LEADER_BEHIND_DIST = 2.0f;
        private SteeringForArrive arriveScript;
        private Vector3 randomOffset;
        void Start()
        {
            m_vehicle = GetComponent<Vehicle>();
            maxSpeed = m_vehicle.maxSpeed;
            isPlanar = m_vehicle.isPlanar;
            leaderController = leader.GetComponent<Vehicle>();
            arriveScript = GetComponent<SteeringForArrive>();
            arriveScript.target = new GameObject("arriveTarget");
            arriveScript.target.transform.position = leader.transform.position;

        }

        public override Vector3 Force()
        {
            leaderVelocity = leaderController.velocity;
            target = leader.transform.position + LEADER_BEHIND_DIST * (-leaderVelocity).normalized;
            arriveScript.target.transform.position = target;
            return new Vector3(0, 0, 0);
        }

    }
}
