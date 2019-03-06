using System;
using System.Collections.Generic;
using UnityEngine;
using SteeringSys;

namespace FinalStateMachine
{
    public enum ActionList {
        NULL,
        AnimationAct,
        AnimatorAct,
        SteeringAct,
        SteeringSysAct,
        MessageAct
    }

    public class NullAct : StateAction { }
    public class AnimationAct:StateAction,ICloneable
    {
        public AnimationAct(Animation control, string clip)
        {
            this.control = control;
            this.clip = clip;
        }

        [SerializeField]Animation control;
        [SerializeField]string clip;

        public override void DoBeforeEntering()
        {
            control.Play(clip, PlayMode.StopSameLayer);
        }

        public override void DoBeforeLeaving()
        {
            control.Stop();
        }

        public object Clone()
        {
            return new AnimationAct(control, clip);
        }
    }
    public class AnimatorAct:StateAction
    {
        Animator animator;
        public string transName;
        public enum AnimType
        {
            boolean,
            integal,
            floatType
        }
        [SerializeField] private AnimType animType = AnimType.boolean;
        [SerializeField] public bool value0;
        [SerializeField] private int value1;
        [SerializeField] private float value2;
        [SerializeField] private float changeDelta = 0.0f;

        private AnimatorAct(Animator animator, string name)
        {
            this.animator = animator;
            this.transName = name;
        }

        public AnimatorAct(Animator animator,string name,bool value):this( animator, name)
        {
            animType = AnimType.boolean;
            value0 = value;
        }

        public AnimatorAct(Animator animator,string name,int value):this(animator,name)
        {
            animType = AnimType.integal;
            value1 = value;
        }

        public AnimatorAct(Animator animator, string name, float value, float delta = 0.0f) : this(animator, name)
        {
            animType = AnimType.integal;
            value2 = value;
            if (delta <= 0.0f || delta >= 1.0f)
                delta = 0.0f;
            else changeDelta = delta;
        }

        public override void DoBeforeEntering()
        {
            
        }

        public override void Act()
        {}

        public override void DoBeforeLeaving()
        {
            switch (animType)
            {
                case AnimType.boolean:
                    animator.SetBool(transName, value0);
                    break;
                case AnimType.integal:
                    animator.SetInteger(transName, value1);
                    break;
                case AnimType.floatType:
                    animator.SetFloat(transName, value2);
                    break;
            }
        }

    }
    public class SteeringAct:StateAction,ICloneable
    {
        [SerializeField] Steering steering;
        [SerializeField] Vehicle vehicle;

        public SteeringAct(Vehicle vehicle,Steering steering)
        {
            this.steering = steering;
            this.vehicle = vehicle;
        }

        public object Clone()
        {
            return new SteeringAct(vehicle, steering);
        }

        public override void DoBeforeEntering()
        {
            steering.enabled = true;
        }

        public override void DoBeforeLeaving()
        {
            steering.enabled = false;
            vehicle.Stop();
        }
    }
    public class SteeringSysAct:StateAction
    {
        
    }
    public class MessageAct:StateAction
    {
        [SerializeField] GameObject sender;
        [SerializeField] ISendable receiver;
        [SerializeField] MessageType type;
        [SerializeField] float delay;
        [SerializeField] object message;

        public MessageAct(MessageType type,GameObject sender,ISendable receiver,object message,float delay = 0.0f)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.delay = delay;
            this.message = message;
            this.type = type;
        }

        public override void DoBeforeLeaving()
        {
            MessageDispatcher.GetInstance.DispatchMessage(delay, sender, receiver, type, message);
        }
    }
}
