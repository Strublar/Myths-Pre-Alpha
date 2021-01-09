using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class BeginTurnMessage : ServerMessage
    {
        private int playerId;

        public BeginTurnMessage(int playerId)
        {
            this.messageType = ServerMessageType.BeginTurn;
            this.playerId = playerId;
            
        }
        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();

            returnArray = Message.AddByte(this, returnArray, (byte)messageType);
            returnArray = Message.AddInt(this, returnArray, playerId);

            return returnArray;
        }
    }
}
