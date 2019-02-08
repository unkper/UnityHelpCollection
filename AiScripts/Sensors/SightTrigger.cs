using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sensors
{
    public class SightTrigger : Trigger
    {
        public Color color = Color.red;
        public float yOffset = 1.0f;

        public override void Try(Sensor s)
        {
            if (isTouchingTrigger(s))
                s.Notify(this);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = color;
            Vector3 vector = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
            Gizmos.DrawSphere(vector, 0.2f);
            
        }

        protected override bool isTouchingTrigger(Sensor sensor)
        {
            GameObject g = sensor.gameObject;
            if(sensor.sensorType==Sensor.SensorType.sight)
            {
                RaycastHit hit;
                Vector3 rayDirection = transform.position - g.transform.position;
                rayDirection.y = 0;
                if((Vector3.Angle(rayDirection,g.transform.forward))<(sensor as SightSensor).fieldOfView)
                {
                    if (Physics.Raycast(g.transform.position + Vector3.up * (sensor as SightSensor).yOffset, rayDirection, out hit, (sensor as SightSensor).viewDistance))
                        if (hit.collider.gameObject == this.gameObject)
                            return true;
                   
                }
            }
            return false;
        }

        public override void Updateme()
        {
            position = transform.position;
        }

        new void Start()
        {
            base.Start();
            manager.RegisterTrigger(this);
        }

    }
}
