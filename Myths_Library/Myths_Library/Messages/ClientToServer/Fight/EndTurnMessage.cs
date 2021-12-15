using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    public class EndTurnMessage : Message
    {

        public EndTurnMessage()
        {
            this.messageType = (byte)ClientMessageType.EndTurn;
        }



        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();
            returnArray = Message.AddByte(this, returnArray, (byte)messageType);



            return returnArray;
        }

        public override void ParseMessage(byte[] message)
        {
            messageType = ParseByte(message);

        }
    }
}


