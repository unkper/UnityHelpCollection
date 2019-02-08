using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FinalStateMachine;

namespace Sensors
{
    public class SoundSensor : Sensor
    {
        public float thresholdValue = 1.0f; //当声音的阈值低于这个时则表示听不到
        
        void Start()
        {
            sensorType = SensorType.sound;
            manager.RegisterSensor(this);
        }

        public override void Notify(Trigger t)
        {
            print("i hear some sound at" + t.gameObject.transform.position + Time.time);
            MessageDispatcher.GetInstance.DispatchMessage(0.0f, this, t.GetComponent<ISendable>(), MessageType.Sound, gameObject);
        }
    }
}
