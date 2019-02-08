using System;
using System.Collections.Generic;
using UnityEngine;
using Timers;
using AIscripts;

namespace FinalStateMachine
{
    public class NullTransition:StateTransition
    {
        public NullTransition(int prior) : base(prior) { }

        public override void AddTransitions<T>(SimplifyState<T> simplifyState) { }

        public override Transition Reason()
        {
            return Transition.NullTransition;
        }
    }

    public class OneTransition:StateTransition
    {
        StateID gotoState;
        protected Transition happenTrans;

        public OneTransition(int prior, Transition trans,StateID gotoState):base(prior)
        {
            this.gotoState = gotoState;
            this.happenTrans = trans;
        }

        public override void AddTransitions<T>(SimplifyState<T> simplifyState)
        {
            simplifyState.AddTransition(happenTrans, gotoState);
        }
    }

    public class TimeTransition : OneTransition
    {
        NormalTime normalTime;

        public TimeTransition(int prior,Transition trans,StateID state,int leftTime):base(prior,trans,state)
        {
            normalTime = new NormalTime(leftTime);
        }

        public override void Enter()
        {
            normalTime.Timing();
        }

        public override void Act()
        {
            normalTime.Update();
        }

        public override Transition Reason()
        {
            if (normalTime.finish)
                return happenTrans;
            return Transition.NullTransition;
        }

    }

    public class SteerTransition : OneTransition
    {
        IArriveSteering arriveSteering;
        public SteerTransition(int prior, Transition trans, StateID gotoState, IArriveSteering steer) : base(prior, trans, gotoState)
        {
            arriveSteering = steer;
        }

        public override Transition Reason()
        {
            if(arriveSteering.hasArrive())
                return happenTrans;
            return Transition.NullTransition;
        }

    }
}
