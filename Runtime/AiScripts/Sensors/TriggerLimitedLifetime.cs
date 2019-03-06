using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sensors
{
    public class TriggerLimitedLifetime : Trigger
    {
        public float lifetime;
        public override void Updateme()
        {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0.0f)
                toBeRemoved = true;
        }

        new void Start()
        {
            base.Start();
        }

    }
}
