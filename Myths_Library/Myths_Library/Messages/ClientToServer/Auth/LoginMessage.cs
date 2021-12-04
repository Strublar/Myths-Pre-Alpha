using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    public class LoginMessage : Message
    {
        public string username;

        public LoginMessage()
        {
            this.messageType = (byte)ClientMessageType.Login;
        }

        public LoginMessage(string username)
        {
            this.messageType = (byte)ClientMessageType.Login;
            this.username = username;
        }

        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();
            returnArray = Message.AddByte(this, returnArray, (byte)messageType);
            returnArray = Message.AddString(this, returnArray, username);
            return returnArray;
        }

        public override void ParseMessage(byte[] message)
        {
            username = Encoding.UTF8.GetString(message, 1, message.Length - 1);
        }
    }
}


