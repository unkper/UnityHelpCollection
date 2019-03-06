using System;
using System.Collections.Generic;
using UnityEngine;
using Tools;

namespace FinalStateMachine
{
    //这个类必须挂载在一个gameobject里做游戏更新
    public class MessageDispatcher
    {
        private PriorityQueue<Telegram> Telegrams = new PriorityQueue<Telegram>(10);
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
                Telegrams.Push(telegram);
            }     
        }

        public void DispatchDelayedMessage()
        {
            if(Time.time >= Telegrams.Top().DispatchTime)
            {
                var t = Telegrams.Pop();
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
