using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    public class MatchFoundMessage : Message
    {
        public MatchFoundMessage()
        {
            this.messageType = (byte)ServerMessageType.MatchFound;
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


