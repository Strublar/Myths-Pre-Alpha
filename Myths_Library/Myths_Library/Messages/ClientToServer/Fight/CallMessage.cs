using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    public class CallMessage : Message
    {
        public int targetId;
        public int playerId;
        public int x, y;
        public bool isSwitch;

        public CallMessage()
        {
            this.messageType = (byte)ClientMessageType.Call;
        }

        public CallMessage(int targetId, int playerId, int x, int y, bool isSwitch)
        {
            this.messageType = (byte)ClientMessageType.Call;
            this.targetId = targetId;
            this.playerId = playerId;
            this.x = x;
            this.y = y;
            this.isSwitch = isSwitch;
        }

        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();
            returnArray = Message.AddByte(this, returnArray, (byte)messageType);

            returnArray = Message.AddInt(this, returnArray, targetId);
            returnArray = Message.AddInt(this, returnArray, playerId);
            returnArray = Message.AddInt(this, returnArray, x);
            returnArray = Message.AddInt(this, returnArray, y);
            returnArray = Message.AddBool(this, returnArray, isSwitch);

            return returnArray;
        }

        public override void ParseMessage(byte[] message)
        {
            messageType = ParseByte(message);
            targetId = ParseInt(message);
            playerId = ParseInt(message);
            x = ParseInt(message);
            y = ParseInt(message);
            isSwitch = ParseBool(message);
        }
    }
}


