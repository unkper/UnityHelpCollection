using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EventEditor
{
    public class UITrigger : m_Trigger
    {
        public Button button;
        //public Dropdown dropdown;

        void Start()
        {
            button.onClick.AddListener(SendMes);
            //dropdown.onValueChanged.AddListener(SendMes);
        }

        void SendMes()
        {
            Send(this, button);
        }

        void SendMes(int dropValue)
        {
            //Send(this, dropdown);
        }

    }
}

