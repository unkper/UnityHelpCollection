using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sensors
{
    public class Trigger : MonoBehaviour
    {
        public TriggerSys manager;
        protected Vector3 position;
        [HideInInspector]public bool toBeRemoved;

        public virtual void Try(Sensor s)
        { }

        public virtual void Updateme() { }

        protected virtual bool isTouchingTrigger(Sensor sensor)
        {
            return false;
        }

        protected void Start()
        {
            toBeRemoved = false;
        }

        void Update() { }

        

       
    }


}

