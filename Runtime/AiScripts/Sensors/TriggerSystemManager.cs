using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Sensors
{
    public class TriggerSys:MonoBehaviour
    {
        List<Sensor> currentSensors = new List<Sensor>();
        List<Trigger> currentTrigger = new List<Trigger>();
        List<Sensor> sensorsToRemove;
        List<Trigger> triggersToRemove;
        private static TriggerSys instance;
        public TriggerSys GetInstance
        {
            get
            {
                if(!instance)
                    instance = new TriggerSys();
                return instance;
            }
        }

        private TriggerSys() { }

        void Start()
        {
            sensorsToRemove = new List<Sensor>();
            triggersToRemove = new List<Trigger>();
        }

        private void UpdateTriggers()
        {
            foreach(Trigger t in currentTrigger)
            {
                if(t.toBeRemoved)
                {
                    triggersToRemove.Add(t);
                }
                else
                {
                    t.Updateme();
                }
            }
            foreach (Trigger t in triggersToRemove)
                currentTrigger.Remove(t);
        }

        private void TryTriggers()
        {
            foreach(Sensor s in currentSensors)
            {
                if(s.gameObject!=null)
                {
                    foreach(Trigger t in currentTrigger)
                    {
                        t.Try(s);
                    }
                }
                else
                {
                    sensorsToRemove.Add(s);
                }
            }
            foreach (Sensor t in sensorsToRemove)
                currentSensors.Remove(t);
        }

        void Update()
        {
            UpdateTriggers();
            TryTriggers();
        }

        public void RegisterTrigger(Trigger t)
        {
            currentTrigger.Add(t);
        }

        public void RegisterSensor(Sensor s)
        {
            currentSensors.Add(s);
        }

    }
}
