using UnityEngine;
using System.Collections.Generic;
using System.Collections;


namespace AIscripts
{
    public class AILocomotion:Vehicle
    {
        public bool ifTurning = true;
        private CharacterController controller;
        private Rigidbody theRigidbody;
        private Animator animator;

        private Vector3 moveDistance;
        private float sumDegree;
        
        
        void Start()
        {
            controller = GetComponent<CharacterController>();
            theRigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            moveDistance = new Vector3(0, 0, 0);
            sumDegree = 0.0f;
        }

        void plusDegree(float deltaDegree)
        {
            if (sumDegree >= 90 && deltaDegree > 0)
                return;
            if (sumDegree <= -90 && deltaDegree < 0)
                return;
            sumDegree += deltaDegree;
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
                /*if(animator)
                {
                    float degree = Vector3.SignedAngle(transform.forward, newForward, transform.up);
                    plusDegree(degree);
                    animator.SetFloat("WalkValue", sumDegree);
                }*/
                if (isPlanar)
                {
                    newForward.y = 0;
                }
                transform.forward = newForward;
            }
            //
        }


    }
}
