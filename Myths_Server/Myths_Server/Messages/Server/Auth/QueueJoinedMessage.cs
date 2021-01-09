using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    public class QueueJoinedMessage : ServerMessage
    {

        public QueueJoinedMessage()
        {
            this.messageType = ServerMessageType.QueueJoined;
        }
        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();
            returnArray = Message.AddByte(this, returnArray, (byte)messageType);
            return returnArray;
        }
    }
}
