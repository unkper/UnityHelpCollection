using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FinalStateMachine;

namespace Sensors
{
    public class SightSensor : Sensor
    {
        public float fieldOfView = 45.0f;
        public float viewDistance = 100.0f;
        public float yOffset = 1.0f;
        void Start()
        {
            sensorType = SensorType.sight;
            manager.RegisterSensor(this);
        }

        public override void Notify(Trigger t)
        {
            Debug.Log("I see a " + t.gameObject.name + "!");
            Debug.DrawLine(transform.position, t.transform.position, Color.red);
            //做出相应行为
            MessageDispatcher.GetInstance.DispatchMessage(0.0f, this, t.GetComponent<ISendable>(), MessageType.Sight, gameObject);
        }

        void OnDrawGizmos()
        {
            Vector3 frontRayPoint = transform.position + (transform.forward * viewDistance);
            //角度值换算弧度制
            float fieldOfViewinRadians = fieldOfView * 3.1415f / 180.0f;
            Vector3 leftRayPoint = transform.TransformPoint(new Vector3(viewDistance * Mathf.Sin(fieldOfViewinRadians), 0, viewDistance * Mathf.Cos(fieldOfViewinRadians)));
            Vector3 rightRayPoint = transform.TransformPoint(new Vector3(-viewDistance * Mathf.Sin(fieldOfViewinRadians), 0, viewDistance * Mathf.Cos(fieldOfViewinRadians)));
            Vector3 yOff = Vector3.up * yOffset;
            Debug.DrawLine(transform.position + yOff, frontRayPoint+ yOff, Color.green);
            Debug.DrawLine(transform.position + yOff, leftRayPoint + yOff, Color.green);
            Debug.DrawLine(transform.position + yOff, rightRayPoint + yOff, Color.green);
        }

    }
}
