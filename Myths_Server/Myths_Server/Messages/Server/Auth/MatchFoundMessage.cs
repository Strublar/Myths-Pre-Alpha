using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    public class MatchFoundMessage : ServerMessage
    {

        public MatchFoundMessage()
        {
            this.messageType = ServerMessageType.MatchFound;
        }
        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();
            returnArray = Message.AddByte(this, returnArray, (byte)messageType);
            return returnArray;
        }
    }
}
