using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EntityCastSpellMessage : ServerMessage
    {
        private int casterId,spellId,x,y;

        public EntityCastSpellMessage(int casterId,int spellId,int x, int y)
        {
            this.messageType = ServerMessageType.SpellCast;
            this.casterId = casterId;
            this.spellId = spellId;
            this.x = x;
            this.y = y;

        }
        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();

            returnArray = Message.AddByte(this, returnArray, (byte)messageType);
            returnArray = Message.AddInt(this, returnArray, casterId);
            returnArray = Message.AddInt(this, returnArray, spellId);
            returnArray = Message.AddInt(this, returnArray, x);
            returnArray = Message.AddInt(this, returnArray, y);
            return returnArray;
        }
    }
}
