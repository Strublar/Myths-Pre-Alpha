using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    public class UnCallMessage : Message
    {
        public int targetId;
        public int playerId;

        public UnCallMessage()
        {
            this.messageType = (byte)ClientMessageType.Recall;
        }

        public UnCallMessage(int targetId, int playerId)
        {
            this.messageType = (byte)ClientMessageType.Recall;
            this.targetId = targetId;
            this.playerId = playerId;

        }

        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();
            returnArray = Message.AddByte(this, returnArray, (byte)messageType);

            returnArray = Message.AddInt(this, returnArray, targetId);
            returnArray = Message.AddInt(this, returnArray, playerId);


            return returnArray;
        }

        public override void ParseMessage(byte[] message)
        {
            messageType = ParseByte(message);
            targetId = ParseInt(message);
            playerId = ParseInt(message);

        }
    }
}


