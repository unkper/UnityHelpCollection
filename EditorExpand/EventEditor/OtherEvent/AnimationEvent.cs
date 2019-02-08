using System;
using System.Collections.Generic;
using UnityEngine;

namespace EventEditor
{
    [RequireComponent(typeof(Animation))]
    public class AnimationEvent : m_Event
    {
        private Animation controller;
        public QueueMode queueMode = QueueMode.CompleteOthers;
        public PlayMode playMode = PlayMode.StopAll;
        public AnimationClip[] PlayClips = new AnimationClip[2];

        void Start()
        {
            controller = GetComponent<Animation>();
            int i = 0;
            foreach(var v in PlayClips)
                controller.AddClip(v, "AddClip" + i++);
        }

        public override void PlayEvents(m_Trigger sender, object info)
        {
            int len = controller.GetClipCount();
            for(int i=0;i<len;i++)
            {
                controller.PlayQueued("AddClip" + i, queueMode, playMode);
            }      
        }
    }
}
