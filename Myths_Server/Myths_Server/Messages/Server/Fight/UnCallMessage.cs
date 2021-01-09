using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class UnCallMessage : ServerMessage
    {
        private int targetId;

        public UnCallMessage(int targetId)
        {
            this.messageType = ServerMessageType.UnitUncalled;
            this.targetId = targetId;
            
        }
        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();

            returnArray = Message.AddByte(this, returnArray, (byte)messageType);
            returnArray = Message.AddInt(this, returnArray, targetId);

            return returnArray;
        }
    }
}
