using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EntityStatChangedMessage : ServerMessage
    {
        int targetId;
        Stat stat;
        int newValue;

        public EntityStatChangedMessage(int targetId,Stat stat,int newValue)
        {
            this.messageType = ServerMessageType.EntityStatChanged;
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
    }
}
