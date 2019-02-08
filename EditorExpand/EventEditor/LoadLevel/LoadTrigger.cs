using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace EventEditor
{
    public class LoadTrigger : m_Trigger {
        // Use this for initialization
        void Start()
        {
            Send(this, null);
        }
    }
}

