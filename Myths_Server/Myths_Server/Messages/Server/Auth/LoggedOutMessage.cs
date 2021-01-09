using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    public class LoggedOutMessage : ServerMessage
    {
        public LoggedOutMessage()
        {
            this.messageType = ServerMessageType.LoggedOut;
        }
        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();
            returnArray = Message.AddByte(this, returnArray, (byte)messageType);
            return returnArray;
        }
    }
}
