using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventEditor
{
    public class EnabledEvent : m_Event
    {
        public bool EnableOrDisable = true;
        public float delayTime = 0.0f;
        public GameObject controlThing;

        public override void PlayEvents(m_Trigger sender, object info)
        {
            if (delayTime == 0.0f)
                controlThing.SetActive(EnableOrDisable);
            else
                StartCoroutine(Delay());
        }

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(delayTime);
            controlThing.SetActive(EnableOrDisable);
        }
    }
}

