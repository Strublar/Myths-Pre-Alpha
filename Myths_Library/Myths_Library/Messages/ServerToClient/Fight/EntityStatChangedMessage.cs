using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    public class EntityStatChangedMessage : Message
    {
        public int targetId;
        public Stat stat;
        public int newValue;
        public EntityStatChangedMessage()
        {
            this.messageType = (byte)ServerMessageType.EntityStatChanged;
        }

        public EntityStatChangedMessage(int targetId, Stat stat, int newValue)
        {
            this.messageType = (byte)ServerMessageType.EntityStatChanged;
            this.targetId = targetId;
            this.stat = stat;
            this.newValue = newValue;
        }

        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();
            returnArray = Message.AddByte(this, returnArray, (byte)messageType);

            returnArray = Message.AddInt(this, returnArray, targetId);
            returnArray = Message.AddByte(this, returnArray, (byte)stat);
            returnArray = Message.AddInt(this, returnArray, newValue);

            return returnArray;
        }

        public override void ParseMessage(byte[] message)
        {
            messageType = ParseByte(message);
            targetId = ParseInt(message);
            stat = (Stat)ParseByte(message);
            newValue = ParseInt(message);

        }
    }
}


