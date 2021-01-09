using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    public class LoggedInMessage : ServerMessage
    {
        private string username;
        public LoggedInMessage(string username)
        {
            this.messageType = ServerMessageType.LoggedIn;
            this.username = username;
        }

        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();
            returnArray = Message.AddByte(this,returnArray, (byte)messageType);
            returnArray = Message.AddString(this,returnArray, username);
            return returnArray;
        }
    }
}
