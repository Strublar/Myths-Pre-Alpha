using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EndGameMessage : ServerMessage
    {
        private int winnerId;

        public EndGameMessage(int winnerId)
        {
            this.messageType = ServerMessageType.EndGame;
            this.winnerId = winnerId;
            
        }
        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();

            returnArray = Message.AddByte(this, returnArray, (byte)messageType);
            returnArray = Message.AddInt(this, returnArray, winnerId);

            return returnArray;
        }
    }
}
