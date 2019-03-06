using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace FinalStateMachine
{
    public abstract class StateFactoryBase
    {
        public abstract StateAction GetNewAction<T>(ActionList id);
        public abstract StateTransition GetNewTransition(TransitionList id);
    }

    public class StateFactory1 : StateFactoryBase
    {
        public override StateAction GetNewAction<T>(ActionList id)
        {
            switch (id)
            {
                case ActionList.AnimatorAct: return new AnimatorAct(null,null,false);
                case ActionList.MessageAct: return new MessageAct(MessageType.NullMessage, null, null, null, 0.0f);
                case ActionList.SteeringAct: return new SteeringAct(null, null);
                default: Debug.LogError("不支持生成"); return null; 
            }
        }

        public override StateTransition GetNewTransition(TransitionList id)
        {
            switch (id)
            {
                case TransitionList.BooleanTransition: return new BooleanTransition(1,Transition.NullTransition,StateID.NullStateID,false,false);
                case TransitionList.TimeTransition: return new TimeTransition(1,Transition.NullTransition,StateID.NullStateID,0);
                default: Debug.LogError("不支持生成"); return null;
            }
        }
    }

}
