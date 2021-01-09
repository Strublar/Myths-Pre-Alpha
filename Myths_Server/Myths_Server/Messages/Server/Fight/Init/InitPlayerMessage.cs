using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    public class InitPlayerMessage : ServerMessage
    {

        private int playerId;
        private int entityId;
        private bool isLocalPlayer;
        private string playerName;
        public InitPlayerMessage(int playerId,int entityId,bool isLocalPlayer,string playerName)
        {
            this.messageType = ServerMessageType.InitPlayer;
            this.playerId = playerId;
            this.entityId = entityId;
            this.isLocalPlayer = isLocalPlayer;
            this.playerName = playerName;
        }
        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();
            returnArray = Message.AddByte(this, returnArray, (byte)messageType);
            returnArray = Message.AddInt(this, returnArray, playerId);
            returnArray = Message.AddInt(this, returnArray, entityId);
            returnArray = Message.AddBool(this, returnArray, isLocalPlayer);
            returnArray = Message.AddString(this, returnArray, playerName);
            return returnArray;
        }
    }
}
