using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SteeringSys
{
    /// <summary>
    /// 为了不发生绕着一个点转的情况，ai的速度必须小或者stoppingDis必须足够大
    /// </summary>
    public class SteeringFollowPath : Steering,ITargetsSteering,IArriveSteering
    {
        public bool ifWhile = false;
        public bool isPlanar = true;
        public List<Transform> linePoints = new List<Transform>();
        public Color pointColor = Color.red;
        public float pointRadius;
        public int AttCurrentNode { get { return currentNode; } set { if (value >= 0 && value < numberOfNodes) currentNode = value; } }
        public string whereHasPath;
        public UnityEvent Arrive;

        public float slowDownDistance = 0.3f;
        public float arrivalDistance = 0.1f;
        private float sqrArriveDis;
        private Transform target;
        private int currentNode = 0;
        private int numberOfNodes;
        private Vector3 desiredVelocity;
        private Vehicle m_vehicle;
        private float maxSpeed;


        void Start()
        {
            m_vehicle = GetComponent<Vehicle>();
            slowDownDistance = m_vehicle.SlowDownDis;
            arrivalDistance = m_vehicle.StoppingDis;
            sqrArriveDis = arrivalDistance * arrivalDistance;
            maxSpeed = m_vehicle.maxSpeed;
            isPlanar = m_vehicle.isPlanar;
            var g = GameObject.Find(whereHasPath);
            linePoints.Clear();
            foreach (Transform t in g.transform)
            {
                linePoints.Add(t);
            }
            numberOfNodes = linePoints.Count;
            if (numberOfNodes!=0)
                target = linePoints[currentNode];
        } 

        public override Vector3 Force()
        {
            Vector3 force = new Vector3(0, 0, 0);
            if(target==null)
            {
                Debug.LogError("无目标");
                return force;
            }
            if(isPlanar)
            {
                force.y = 0;
            }
            Vector3 dist = target.position - transform.position;
            if(currentNode==numberOfNodes-1)
            {
                if(dist.magnitude>slowDownDistance)
                {
                    desiredVelocity = dist.normalized * maxSpeed;
                    force = desiredVelocity - m_vehicle.velocity;
                }
                else if(dist.magnitude>arrivalDistance)
                {
                    desiredVelocity = dist - m_vehicle.velocity;
                    force = desiredVelocity - m_vehicle.velocity;
                }
                else
                {
                    if (Arrive != null)
                        Arrive.Invoke();
                    if(ifWhile)
                    {
                        currentNode = 0;
                        target = linePoints[currentNode];
                    }
                }
            }
            else
            {
                if(dist.sqrMagnitude<sqrArriveDis)
                {
                    currentNode++;
                    target = linePoints[currentNode].transform;
                }
                if (dist.magnitude > slowDownDistance)
                {
                    desiredVelocity = dist.normalized * maxSpeed;
                    force = desiredVelocity - m_vehicle.velocity;
                }
            }
            return force;
        }

        void OnDrawGizmos()
        {
            if (linePoints.Count == 0)
                return;
            foreach (Transform t in linePoints)
                Gizmos.DrawSphere(t.position, pointRadius);
        }

        public IList<Transform> GetTargets()
        {
            return this.linePoints;
        }

        public void SetTargets(IList<Transform> targets)
        {
            this.linePoints.Clear();
            linePoints.AddRange(targets);
            currentNode = 0;
            target = linePoints[currentNode];
        }

        public UnityEvent hasArrive()
        {
            return Arrive;
        }

        public int GetCurTargetIdx()
        {
            return currentNode;
        }
    }
}
