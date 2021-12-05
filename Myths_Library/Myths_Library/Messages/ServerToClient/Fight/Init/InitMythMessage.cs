using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    public class InitMythMessage : Message
    {
        public int playerId;
        public int teamIndex;
        public MythSet set;
        public InitMythMessage()
        {
            this.messageType = (byte)ServerMessageType.InitPlayer;
        }

        public InitMythMessage(int playerId, int teamIndex, MythSet set)
        {
            this.messageType = (byte)ServerMessageType.InitPlayer;
            this.set = set;
            this.playerId = playerId;
            this.teamIndex = teamIndex;
        }

        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();
            returnArray = Message.AddByte(this, returnArray, (byte)messageType);

            returnArray = Message.AddInt(this, returnArray, playerId);
            returnArray = Message.AddInt(this, returnArray, teamIndex);
            returnArray = Message.AddInt(this, returnArray, set.id);
            returnArray = Message.AddByte(this, returnArray, set.passive);

            returnArray = Message.AddByte(this, returnArray, set.spells[0]);
            returnArray = Message.AddByte(this, returnArray, set.spells[1]);
            returnArray = Message.AddByte(this, returnArray, set.spells[2]);
            returnArray = Message.AddByte(this, returnArray, set.spells[3]);
            return returnArray;
        }

        public override void ParseMessage(byte[] message)
        {
            messageType = ParseByte(message);
            set = new MythSet();
            playerId = ParseInt(message);
            teamIndex = ParseInt(message);

            set.id = ParseInt(message);
            set.passive = ParseByte(message);
            set.spells[0] = ParseByte(message);
            set.spells[1] = ParseByte(message);
            set.spells[2] = ParseByte(message);
            set.spells[3] = ParseByte(message);

        }
    }
}


