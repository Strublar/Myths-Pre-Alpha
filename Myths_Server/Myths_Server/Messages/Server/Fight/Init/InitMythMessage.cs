using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    public class InitMythMessage : ServerMessage
    {
        private int playerId;
        private byte teamIndex;
        private int entityId;
        private int unitId;
        private int hp, armor, barrier, attack, range, attackType, mobility;
        public InitMythMessage(int playerId,byte teamIndex,int entityId,int unitId,
            int hp = 0,int armor = 0, int barrier = 0, int attack = 0, int range = 1, int attackType = 1, int mobility = 2)
        {
            this.messageType = ServerMessageType.InitMyth ;
            this.playerId = playerId;
            this.teamIndex = teamIndex;
            this.entityId = entityId;
            this.unitId = unitId;
            this.hp = hp;
            this.armor = armor;
            this.barrier = barrier;
            this.attack = attack;
            this.range = range;
            this.attackType = attackType;
            this.mobility = mobility;
        }

        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();

            returnArray = Message.AddByte(this, returnArray, (byte)messageType);
            returnArray = Message.AddInt(this, returnArray, playerId);
            returnArray = Message.AddByte(this, returnArray, teamIndex);
            returnArray = Message.AddInt(this, returnArray, entityId);
            returnArray = Message.AddInt(this, returnArray, unitId);
            returnArray = Message.AddInt(this, returnArray, hp);
            returnArray = Message.AddInt(this, returnArray, armor);
            returnArray = Message.AddInt(this, returnArray, barrier);
            returnArray = Message.AddInt(this, returnArray, attack);
            returnArray = Message.AddInt(this, returnArray, range);
            returnArray = Message.AddInt(this, returnArray, attackType);
            returnArray = Message.AddInt(this, returnArray, mobility);
            return returnArray;
        }
    }
}
