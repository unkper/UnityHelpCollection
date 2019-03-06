using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sensors
{
    public class Sensor : MonoBehaviour
    {
        public TriggerSys manager;
        //add sensor's type from here
        public enum SensorType
        {
            sight,
            sound,
            health
        }

        public SensorType sensorType;
        public virtual void Notify(Trigger t) { }

    }


}

