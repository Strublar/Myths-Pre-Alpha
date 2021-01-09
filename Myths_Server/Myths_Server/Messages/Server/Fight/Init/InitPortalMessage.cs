using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    public class InitPortalMessage : ServerMessage
    {

        private int entityId;
        private int x,y;
        public InitPortalMessage(int entityId, int x,int y)
        {
            this.messageType = ServerMessageType.InitPortal ;
            this.entityId = entityId;
            this.x = x;
            this.y = y;

        }

        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();

            returnArray = Message.AddByte(this, returnArray, (byte)messageType);
            returnArray = Message.AddInt(this, returnArray, entityId);
            returnArray = Message.AddInt(this, returnArray, x);
            returnArray = Message.AddInt(this, returnArray, y);

            return returnArray;
        }
    }
}
