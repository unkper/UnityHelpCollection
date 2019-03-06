using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sensors
{
    public class SoundTrigger : TriggerLimitedLifetime
    {
        public float radius;
        public override void Try(Sensor s)
        {
            if (isTouchingTrigger(s))
                s.Notify(this);
        }

        protected override bool isTouchingTrigger(Sensor sensor)
        {
            GameObject g = sensor.gameObject;
            if(sensor.sensorType==Sensor.SensorType.sound)
            {
                if ((Vector3.Distance(transform.position, g.transform.position)) < radius)
                    return true;
      
            }
            return false;
        }

        new void Start()
        {
            base.Start();
            manager.RegisterTrigger(this);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

    }
}
