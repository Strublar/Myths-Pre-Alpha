using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class CapturePortalMessage : ServerMessage
    {
        private int portalId, team;

        public CapturePortalMessage(int portalId, int team)
        {
            this.messageType = ServerMessageType.CapturePortal;
            this.portalId = portalId;
            this.team = team;
            
        }
        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();

            returnArray = Message.AddByte(this, returnArray, (byte)messageType);
            returnArray = Message.AddInt(this, returnArray, portalId);
            returnArray = Message.AddInt(this, returnArray, team);
            return returnArray;
        }
    }
}
