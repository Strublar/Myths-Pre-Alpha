using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    public class MoveMessage : Message
    {
        public int targetId;
        public int x, y;

        public MoveMessage()
        {
            this.messageType = (byte)ClientMessageType.Move;
        }

        public MoveMessage(int targetId, int x, int y)
        {
            this.messageType = (byte)ClientMessageType.Move;
            this.targetId = targetId;
            this.x = x;
            this.y = y;
        }

        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();
            returnArray = Message.AddByte(this, returnArray, (byte)messageType);

            returnArray = Message.AddInt(this, returnArray, targetId);
            returnArray = Message.AddInt(this, returnArray, x);
            returnArray = Message.AddInt(this, returnArray, y);

            return returnArray;
        }

        public override void ParseMessage(byte[] message)
        {
            messageType = ParseByte(message);
            targetId = ParseInt(message);
            x = ParseInt(message);
            y = ParseInt(message);
        }
    }
}


