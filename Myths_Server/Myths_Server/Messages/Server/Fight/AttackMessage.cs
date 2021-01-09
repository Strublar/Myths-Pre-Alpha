using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class AttackMessage : ServerMessage
    {
        private int targetId;

        public AttackMessage(int targetId)
        {
            this.messageType = ServerMessageType.UnitAttack;
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
