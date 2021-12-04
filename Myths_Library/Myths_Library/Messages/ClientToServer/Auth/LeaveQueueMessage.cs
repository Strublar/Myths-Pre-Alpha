using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    public class LeaveQueueMessage : Message
    {
        public LeaveQueueMessage()
        {
            this.messageType = (byte)ClientMessageType.LeaveQueue;
        }

        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();
            returnArray = Message.AddByte(this, returnArray, (byte)messageType);
            return returnArray;
        }

        public override void ParseMessage(byte[] message)
        {

        }
    }
}


