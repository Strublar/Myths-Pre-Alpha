using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    public class EntityUnCalledMessage : Message
    {
        public int targetId;
        public EntityUnCalledMessage()
        {
            this.messageType = (byte)ServerMessageType.UnitUncalled;
        }

        public EntityUnCalledMessage(int targetId)
        {
            this.messageType = (byte)ServerMessageType.UnitUncalled;
            this.targetId = targetId;

        }

        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();
            returnArray = Message.AddByte(this, returnArray, (byte)messageType);

            returnArray = Message.AddInt(this, returnArray, targetId);


            return returnArray;
        }

        public override void ParseMessage(byte[] message)
        {
            messageType = ParseByte(message);

            targetId = ParseInt(message);


        }
    }
}


