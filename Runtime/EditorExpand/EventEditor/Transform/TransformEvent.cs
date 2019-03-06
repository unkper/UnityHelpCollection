using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

namespace EventEditor {
    public class TransformEvent :m_Event
    {
        public PositionMover mover;
        public bool ifEnableObject = false;
        public bool enableIt = true;
        public GameObject enableObject;
        public override void PlayEvents(m_Trigger sender, object info)
        {
            if (!ifEnableObject)
                mover.readyToMove = true;
            else
                enableObject.SetActive(enableIt);
        }
    }
}
