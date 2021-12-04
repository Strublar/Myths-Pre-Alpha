using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    public class LogoutMessage : Message
    {
        public LogoutMessage()
        {
            this.messageType = (byte)ClientMessageType.Logout;
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


