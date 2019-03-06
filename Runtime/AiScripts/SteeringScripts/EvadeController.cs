using UnityEngine;
using System.Collections;

namespace SteeringSys
{
    [RequireComponent(typeof(SteeringForLeaderFollowing))]
    public class EvadeController:MonoBehaviour
    {
        public GameObject leader;
        private Vehicle leaderLocomotion;
        private Vehicle m_vehicle;
        private bool isPlanar;
        private Vector3 leaderAhead;
        public float LEADER_BEHEAD_DIST;
        private Vector3 dist;
        public float evadeDistance;
        private float sqrEvadeDistance;
        private SteeringForEvade evadeScript;

        void Start()
        {
            leaderLocomotion = leader.GetComponent<Vehicle>();
            evadeScript = GetComponent<SteeringForEvade>();
            m_vehicle = GetComponent<Vehicle>();
            isPlanar = m_vehicle.isPlanar;
            LEADER_BEHEAD_DIST = GetComponent<SteeringForLeaderFollowing>().LEADER_BEHIND_DIST;
            sqrEvadeDistance = evadeDistance * evadeDistance;
        }

        void Update()
        {
            leaderAhead = leader.transform.position + leaderLocomotion.velocity.normalized * LEADER_BEHEAD_DIST;
            dist = transform.position - leaderAhead;
            if (isPlanar)
                dist.y = 0;
            if(dist.sqrMagnitude<sqrEvadeDistance)
            {
                evadeScript.enabled = true;
                Debug.DrawLine(transform.position, leader.transform.position);
            }
            else
            {
                evadeScript.enabled = false;
            }
        }
    }
}
