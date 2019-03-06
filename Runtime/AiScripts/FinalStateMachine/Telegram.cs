using System;
using System.Collections.Generic;
using UnityEngine;

namespace FinalStateMachine
{
    public interface ISendable
    {
        bool HandlerMessage(Telegram message);
    }

    public class Telegram:IComparable<Telegram>
    {
        public Telegram(object sender, ISendable receiver, MessageType mes,
            double delayTime,object extraInfo){
            Sender = sender;
            Receiver = receiver;
            Mes = mes;
            DelayTime = delayTime;
            ExtraInfo = extraInfo;
        }

        public double DispatchTime;
        public object Sender;
        public ISendable Receiver;
        public MessageType Mes;
        public double DelayTime;
        public object ExtraInfo;

        public override string ToString()
        {
            return "Receiver:"+ this.Receiver + "   " + this.Mes + "   ";
        }

        public int CompareTo(Telegram other)
        {
            return DispatchTime > other.DispatchTime ? 1 : -1;
        }
    }
}
