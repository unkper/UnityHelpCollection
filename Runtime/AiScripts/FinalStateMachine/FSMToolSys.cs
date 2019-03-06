using System;
using System.Collections.Generic;
using UnityEngine;
using Sensors;

namespace FinalStateMachine
{
    [Serializable]
    public class FSMToolSys:MonoBehaviour
    {
        public FSMSystem<object> fsmSystem = new FSMSystem<object>();
        public List<Sensor> sensors;
        public List<Trigger> triggers;


    }
}
