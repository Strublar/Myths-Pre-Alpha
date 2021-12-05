using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    public class InitPlayerMessage : Message
    {
        public int playerId;
        public int entityId;
        public bool isLocalPlayer;
        public string playerName;
        public InitPlayerMessage()
        {
            this.messageType = (byte)ServerMessageType.InitPlayer;
        }

        public InitPlayerMessage(int playerId, int entityId, bool isLocalPlayer, string playerName)
        {
            this.messageType = (byte)ServerMessageType.InitPlayer;
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

        public override void ParseMessage(byte[] message)
        {
            messageType = ParseByte(message);
            playerId = ParseInt(message);
            entityId = ParseInt(message);
            isLocalPlayer = ParseBool(message);
            playerName = Encoding.UTF8.GetString(message, 10, message.Length - 11);
        }
    }
}


