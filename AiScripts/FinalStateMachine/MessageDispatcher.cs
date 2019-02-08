using System;
using System.Collections.Generic;
using UnityEngine;

namespace FinalStateMachine
{
    //这个类必须挂载在一个gameobject里做游戏更新
    public class MessageDispatcher
    {
        private List<Telegram> Telegrams = new List<Telegram>();
        private static MessageDispatcher instance = new MessageDispatcher();
        public static MessageDispatcher GetInstance { get { return instance; } }

        private MessageDispatcher() { }

        public void DispatchMessage(double delayTime, object sender, ISendable receiver,MessageType message,object extraInfo)
        {
            Telegram telegram = new Telegram(sender, receiver, message, delayTime, extraInfo);
            if (delayTime<=0.0)
            {
                Discharge(telegram);
            }
            else
            {
                telegram.DispatchTime = Time.time + telegram.DelayTime;
                Telegrams.Add(telegram);
            }     
        }

        public void DispatchDelayedMessage()
        {
            List<Telegram> removed = new List<Telegram>();
            foreach(var t in Telegrams)
            {
                t.DispatchTime -= Time.deltaTime;
                if (t.DispatchTime <= 0.0)
                    removed.Add(t);
            }
            foreach(var t in removed)
            {
                Telegrams.Remove(t);
                Discharge(t);
            }
        }

        void Discharge(Telegram telegram)
        {
            if (telegram.Receiver != null)
                telegram.Receiver.HandlerMessage(telegram);
            else
                Debug.LogError("no receiver");
        }
    }
}
