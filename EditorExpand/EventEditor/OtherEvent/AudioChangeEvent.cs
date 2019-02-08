using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EventEditor
{
    public class AudioChangeEvent : m_Event
    {
        public AudioClip changeClip;
        public AudioSource changeSource;

        private void Start()
        {
            changeSource.loop = true;
        }

        public override void PlayEvents(m_Trigger sender, object info)
        {
            
            changeSource.Stop();
            changeSource.clip = changeClip;
            changeSource.Play();
        }
    }
}

