using System;
using System.Collections.Generic;
using UnityEngine;
using Tools;
using SteeringSys;

namespace FinalStateMachine
{
    public enum TransitionList
    {
        NULL,
        TimeTransition,
        BooleanTransition
    }

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
        [SerializeField]StateID gotoState;
        [SerializeField]protected Transition happenTrans;

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
        [SerializeField]NormalTime normalTime;

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

    public class BooleanTransition:OneTransition
    {
        [SerializeField] private Boolean judgeValue;
        [SerializeField] private bool changeValue;
        public BooleanTransition(int prior, Transition trans,StateID state, Boolean value,bool change):base(prior, trans, state)
        {
            judgeValue = value;
            changeValue = change;
        }

        public override Transition Reason()
        {
            if (judgeValue == changeValue)
                return this.happenTrans;
            else return Transition.NullTransition;
        }
    }
}
