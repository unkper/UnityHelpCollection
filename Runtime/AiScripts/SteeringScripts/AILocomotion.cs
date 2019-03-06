using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

namespace SteeringSys
{
    /// <summary>
    /// 根据加速度具体调用三个动力组件运动,其也可被调用来控制物体的运动
    /// </summary>
    public class AILocomotion:Vehicle
    {
        public bool ifTurning = true;
        private CharacterController controller;
        private Rigidbody theRigidbody;
        private Animator animator;

        private Vector3 moveDistance;
        private float sumDegree;

        #region 查找与删除力
        public bool DisableForce<T>()where T:Steering
        {
            var force = GetComponent<T>();
            if (!force) throw new NullReferenceException("没有那种类型的力");
            force.enabled = false;
            return true;
        }

        public bool DisableAllForce()
        {
            var list = GetComponents<Steering>();
            if (list.Length == 0) return false;
            foreach(var force in list)
            {
                force.enabled = false;
            }
            return true;
        }

        public bool DisableAndStop()
        {
            base.Stop();
            return DisableAllForce();
        }
        #endregion

        #region 增添操纵力
        public void ArriveTo(GameObject destination)
        {
            var comp = GetComponent<SteeringForArrive>();
            if(!comp)
                comp = gameObject.AddComponent<SteeringForArrive>();
            comp.arrivalDistance = StoppingDis;
            comp.characterRadius = characterRadius;
            comp.slowDownDistance = SlowDownDis;
            comp.isPlanar = isPlanar;
            comp.target = destination;
        }

        public void FollowPath(List<Transform> path)
        {
            var comp = GetComponent<SteeringFollowPath>();
            if (!comp)
                comp = gameObject.AddComponent<SteeringFollowPath>();
            comp.arrivalDistance = StoppingDis;
            comp.pointRadius = characterRadius;
            comp.slowDownDistance = SlowDownDis;
            comp.isPlanar = isPlanar;
            comp.SetTargets(path);
        }
     
        public void ChaseTo(GameObject target)
        {
            var comp = GetComponent<SteeringForPursuit>();
            if (!comp)
                comp = gameObject.AddComponent<SteeringForPursuit>();
            comp.target = target;
        }

        public void FleeFrom(GameObject target)
        {
            var comp = GetComponent<SteeringForFlee>();
            if (!comp)
                comp = gameObject.AddComponent<SteeringForFlee>();
            comp.target = target;
        }
        /// <summary>
        /// 增添一个操纵力，不改变其默认参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void AddForce<T>()where T : Steering
        {
            if (!GetComponent<T>())
                gameObject.AddComponent<T>();
        }
        #endregion

        void Start()
        {
            controller = GetComponent<CharacterController>();
            theRigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            moveDistance = new Vector3(0, 0, 0);
            sumDegree = 0.0f;
        }

        void FixedUpdate()
        {
            velocity += acceleration * Time.fixedDeltaTime;
            if (velocity.sqrMagnitude > sqrMaxSpeed)
                velocity = velocity.normalized * maxSpeed;
            moveDistance = velocity * Time.fixedDeltaTime;

            if(isPlanar)
            {
                velocity.y = 0;
                moveDistance.y = 0;
            }

            if (controller != null)
                controller.SimpleMove(velocity);
            else if (theRigidbody == null || theRigidbody.isKinematic)
                transform.position += moveDistance;
            else
                theRigidbody.MovePosition(theRigidbody.position + moveDistance);

            if(velocity.sqrMagnitude>0.00001&&ifTurning)
            {
                Vector3 newForward = Vector3.Slerp(transform.forward, velocity, damping * Time.deltaTime);
                if (isPlanar)
                {
                    newForward.y = 0;
                }
                transform.forward = newForward;
            }
            
        }
    }
}
