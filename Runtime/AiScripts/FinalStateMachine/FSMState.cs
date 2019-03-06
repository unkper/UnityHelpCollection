using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

namespace FinalStateMachine
{
    public enum StateID
    {
        NullStateID,
        #region 编辑器状态
        EditIdleMode,
        HintMode,
        BuildMode,
        ConstantBuildMode,
        #endregion
        move,
        spell,
        Global,
        Attack,
        Chase,
        Idle,
        Dodge,
        Died,
        Follow,
        SpSkill,
        Spell,
        Dizzy,
        Scare,
        GetHit,
        Cohision,

    }
    public enum Transition
    {
        NullTransition,
        GotoIdle,
        GotoHint,
        GotoBuild,
        GotoConstantBuild,
        GotoAttack,
        GoFollowPath,
        GoToChase,
        GoToSpell,
        Dizzy,
        Scare,
        GetHit,
    }
    public enum MessageType
    {
        NullMessage,
        Damage,
        Slient,
        Absorb,
        GiveState,
        ImmSlient,
        Sight,
        Sound,
        memory,
    }
    
    /*
     * 这个虚拟类用于怪兽状态的生成，其中T指的是控制怪物的那个类，状态通过Owner与怪物类交互
     */
    [Serializable]
    public abstract class FSMState<T>
    {
        protected Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();
        protected StateID stateID;
        public StateID ID { get { return stateID; } }
        protected T Owner;
        public T GetOwner { get { return Owner; } }


        public FSMState(T owner)
        {
            Owner = owner;         
        }

        public abstract bool OnMessage(Telegram mes);

        public void AddTransition(Transition trans, StateID id)
        {
            // Check if anyone of the args is invalid  
            if (trans == Transition.NullTransition)
            {
                Debug.LogError("FSMState ERROR: NullTransition is not allowed for a real transition");
                return;
            }

            if (id == StateID.NullStateID)
            {
                Debug.LogError("FSMState ERROR: NullStateID is not allowed for a real ID");
                return;
            }

            // Since this is a Deterministic FSM,  
            //   check if the current transition was already inside the map  
            if (map.ContainsKey(trans))
            {
                Debug.LogError("FSMState ERROR: State " + stateID.ToString() + " already has transition " + trans.ToString() +
                               "Impossible to assign to another state");
                return;
            }

            map.Add(trans, id);
        }

        /// <summary>  
        /// This method deletes a pair transition-state from this state's map.  
        /// If the transition was not inside the state's map, an ERROR message is printed.  
        /// </summary>  
        public void DeleteTransition(Transition trans)
        {
            // Check for NullTransition  
            if (trans == Transition.NullTransition)
            {
                Debug.LogError("FSMState ERROR: NullTransition is not allowed");
                return;
            }

            // Check if the pair is inside the map before deleting  
            if (map.ContainsKey(trans))
            {
                map.Remove(trans);
                return;
            }
            Debug.LogError("FSMState ERROR: Transition " + trans.ToString() + " passed to " + stateID.ToString() +
                           " was not on the state's transition list");
        }

        /// <summary>  
        /// This method returns the new state the FSM should be if  
        ///    this state receives a transition and   
        /// </summary>  
        public StateID GetOutputState(Transition trans)
        {
            // Check if the map has this transition  
            if (map.ContainsKey(trans))
            {
                return map[trans];
            }
            return StateID.NullStateID;
        }

        /// <summary>  
        /// This method is used to set up the State condition before entering it.  
        /// It is called automatically by the FSMSystem class before assigning it  
        /// to the current state.  
        /// </summary>  
        public virtual void DoBeforeEntering() { }

        /// <summary>  
        /// This method is used to make anything necessary, as reseting variables  
        /// before the FSMSystem changes to another one. It is called automatically  
        /// by the FSMSystem before changing to a new state.  
        /// </summary>  
        public virtual void DoBeforeLeaving() { }

        /// <summary>  
        /// This method decides if the state should transition to another on its list  
        /// NPC is a reference to the object that is controlled by this class  
        /// </summary>  
        public abstract Transition Reason();

        /// <summary>  
        /// This method controls the behavior of the NPC in the game World.  
        /// Every action, movement or communication the NPC does should be placed here  
        /// NPC is a reference to the object that is controlled by this class  
        /// </summary>  
        public abstract void Act();
    }

    public class SimplifyState<T> : FSMState<T>
    {
        List<StateAction> actions;
        List<StateTransition> transitions;

        public SimplifyState(T owner, StateID stateID, StateAction stateAction, StateTransition transition):base(owner)
        {
            List<StateAction> actions1 = new List<StateAction>();
            List<StateTransition> transitions1 = new List<StateTransition>();
            actions1.Add(stateAction);
            transitions1.Add(transition);
            foreach (var v in transitions1)
                v.AddTransitions(this);
            actions = new List<StateAction>();
            transitions = new List<StateTransition>();
            this.stateID = stateID;
            actions.AddRange(actions1);
            this.transitions.AddRange(transitions1);
            this.transitions.Sort();
        }

        public SimplifyState(T owner, StateID stateID, StateAction action1, StateAction action2, StateTransition transition):base(owner)
        {
            List<StateAction> actions1 = new List<StateAction>();
            List<StateTransition> transitions1 = new List<StateTransition>();
            actions1.AddRange(new StateAction[2]{ action1, action2 });
            transitions1.Add(transition);
            foreach (var v in transitions1)
                v.AddTransitions(this);

            actions = new List<StateAction>();
            transitions = new List<StateTransition>();
            this.stateID = stateID;
            actions.AddRange(actions1);
            transitions.AddRange(transitions1);
            transitions.Sort();
        }

        public SimplifyState(T owner,StateID stateID,IList<StateAction> stateActions,IList<StateTransition> transitions):base(owner)
        {
            actions = new List<StateAction>();
            transitions = new List<StateTransition>();
            foreach (var v in transitions)
                v.AddTransitions(this);
            this.stateID = stateID;
            actions.AddRange(stateActions);
            this.transitions.AddRange(transitions);
            this.transitions.Sort();
        }

        public SimplifyState(T owner,StateID stateID):base(owner)
        {
            actions = new List<StateAction>();
            transitions = new List<StateTransition>();
        }

        public override void Act()
        {
            foreach (var v in transitions)
                v.Act();
            foreach (var v in actions)
                v.Act();
        }

        public override bool OnMessage(Telegram mes)
        {
            foreach (var v in actions)
                v.HandlerMessage(mes);
            foreach (var v in transitions)
                v.HandlerMessage(mes);
            return true;
        }

        public override void DoBeforeEntering()
        {
            foreach (var v in transitions)
                v.Enter();
            foreach (var v in actions)
                v.DoBeforeEntering();
        }

        public override void DoBeforeLeaving()
        {
            foreach (var v in transitions)
                v.Exit();
            foreach (var v in actions)
                v.DoBeforeLeaving();
        }

        public override Transition Reason()
        {
            foreach(var v in transitions)
            {
                var trans = v.Reason();
                Debug.Log(trans);
                if (trans != Transition.NullTransition)
                    return trans;
            }
            return Transition.NullTransition;
        }
    }
    [Serializable]
    public abstract class StateAction:ISendable
    {
        public virtual void DoBeforeEntering() { }
        public virtual void DoBeforeLeaving() { }
        public virtual void Act() { }
        public virtual bool HandlerMessage(Telegram message) { return false; }
    }
    [Serializable]
    public abstract class StateTransition : ISendable,IComparable
    {
        public virtual void Act() { }
        public virtual void Enter() { }
        public virtual void Exit() { }
        public abstract void AddTransitions<T>(SimplifyState<T> simplifyState);
        public virtual Transition Reason() { return Transition.NullTransition; }
        public virtual bool HandlerMessage(Telegram message) { return false; }
        private int prior = 0;

        public StateTransition(int prior)
        {
            this.prior = prior;
        }

        public int CompareTo(object obj)
        {
            return -(this.prior - (obj as StateTransition).prior);
        }
    }

}
