using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    public class QueueLeftMessage : ServerMessage
    {

        public QueueLeftMessage()
        {
            this.messageType = ServerMessageType.QueueLeft;
        }
        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();
            returnArray = Message.AddByte(this, returnArray, (byte)messageType);
            return returnArray;
        }
    }
}
