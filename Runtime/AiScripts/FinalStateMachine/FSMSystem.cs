using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



namespace FinalStateMachine
{ 
    //这个状态机是要求作为一个挂载在对象上的脚本所引用的
    public class FSMSystem<T>:ISendable
    {
        public List<FSMState<T>> states = new List<FSMState<T>>();
        public StateID CurrentStateID { get; private set; }
        public FSMState<T> CurrentState { get; private set; }
        public FSMState<T> GlobalState;
        

        public bool HandlerMessage(Telegram message)
        {
            if (!GlobalState.OnMessage(message))
                return CurrentState.OnMessage(message);
            else return false;
        }

        public void ResetToDefaultState()
        {
            CurrentState = states[0];
            CurrentStateID = states[0].ID;
            CurrentState.DoBeforeEntering();
        }

        private void AddStates(ICollection<FSMState<T>> list)
        {
            if (list.Count == 0)
                Debug.LogError("States count cannot be zero");
            foreach (FSMState<T> state in list)
                states.Add(state);
        }

        public FSMSystem()
        {

        }

        public FSMSystem(IList<FSMState<T>> addStates, FSMState<T> globalState)
        {
            AddStates(addStates);
            GlobalState = globalState;
            ResetToDefaultState();
        }

        public FSMSystem(IList<SimplifyState<T>> addStates,FSMState<T> globalState)
        {
            List<FSMState<T>> list = new List<FSMState<T>>();
            foreach(var v in addStates)
                list.Add(v as FSMState<T>);
            AddStates(list);
            GlobalState = globalState;
            ResetToDefaultState();
        }

        public void Update()
        {
            Transition id = GlobalState.Reason();
            PerformTransition(id,true);
            GlobalState.Act();
            //Debug.Log(states.Count);
            id = CurrentState.Reason();
            PerformTransition(id,false);
            CurrentState.Act();
            
        }

        /// <summary>  
        /// This method places new states inside the FSM,  
        /// or prints an ERROR message if the state was already inside the List.  
        /// First state added is also the initial state.  
        /// </summary>  
        public void AddState(FSMState<T> s)
        {
            // Check for Null reference before deleting  
            if (s == null)
            {
                Debug.LogError("FSM ERROR: Null reference is not allowed");
            }

            // First State inserted is also the Initial state,  
            //   the state the machine is in when the simulation begins  
            if (states.Count == 0)
            {
                states.Add(s);
                CurrentState = s;
                CurrentStateID = s.ID;
                return;
            }

            // Add the state to the List if it's not inside it  
            foreach (FSMState<T> state in states)
            {
                if (state.ID == s.ID)
                {
                    Debug.LogError("FSM ERROR: Impossible to add state " + s.ID.ToString() +
                                   " because state has already been added");
                    return;
                }
            }
            states.Add(s);
        }

        /// <summary>  
        /// This method delete a state from the FSM List if it exists,   
        ///   or prints an ERROR message if the state was not on the List.  
        /// </summary>  
        public void DeleteState(StateID id)
        {
            // Check for NullState before deleting  
            if (id == StateID.NullStateID)
            {
                Debug.LogError("FSM ERROR: NullStateID is not allowed for a real state");
                return;
            }

            // Search the List and delete the state if it's inside it  
            foreach (FSMState<T> state in states)
            {
                if (state.ID == id)
                {
                    states.Remove(state);
                    return;
                }
            }
            Debug.LogError("FSM ERROR: Impossible to delete state " + id.ToString() +
                           ". It was not on the list of states");
        }

        /// <summary>  
        /// This method tries to change the state the FSM is in based on  
        /// the current state and the transition passed. If current state  
        ///  doesn't have a target state for the transition passed,   
        /// an ERROR message is printed.  
        /// </summary>  
        public void PerformTransition(Transition trans,bool ifGlobal)
        {
            // Check for NullTransition before changing the current state  
            if (trans == Transition.NullTransition)
            {
                return;
            }

            // Check if the currentState has the transition passed as argument 
            StateID id;
            if (!ifGlobal)
                id = CurrentState.GetOutputState(trans);
            else id = GlobalState.GetOutputState(trans);
            if (id == StateID.NullStateID)
            {
                Debug.LogError("FSM ERROR: State " + CurrentStateID.ToString() + " does not have a target state " +
                               " for transition " + trans.ToString());
                return;
            }

            // Update the currentStateID and currentState         
            CurrentStateID = id;
            foreach (FSMState<T> state in states)
            {
                if (state.ID == CurrentStateID)
                {
                    // Do the post processing of the state before setting the new one  
                    CurrentState.DoBeforeLeaving();

                    CurrentState = state;

                    // Reset the state to its desired condition before it can reason or act  
                    CurrentState.DoBeforeEntering();
                    break;
                }
            }

        } // PerformTransition()  


    }

    public class DefaultGlobalState<T> : FSMState<T>
    {
        public DefaultGlobalState(T owner) : base(owner) { }

        public override void Act()
        {
            return;
        }

        public override bool OnMessage(Telegram mes)
        {
            return true;
        }

        public override Transition Reason()
        {
            return Transition.NullTransition;
        }
    }
}

