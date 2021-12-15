using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    public class BeginTurnMessage : Message
    {
        public int team;

        public BeginTurnMessage()
        {
            this.messageType = (byte)ServerMessageType.BeginTurn;
        }

        public BeginTurnMessage(int team)
        {
            this.messageType = (byte)ServerMessageType.BeginTurn;
            this.team = team;

        }

        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();
            returnArray = Message.AddByte(this, returnArray, (byte)messageType);

            returnArray = Message.AddInt(this, returnArray, team);

            return returnArray;
        }

        public override void ParseMessage(byte[] message)
        {
            messageType = ParseByte(message);
            team = ParseInt(message);


        }
    }
}


