using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CastSpellMessage : ClientMessage
{
    private int casterId, spellId;
    private int x, y;
    public CastSpellMessage(int casterId, int spellId, int x, int y)
    {
        this.messageType = ClientMessageType.CastSpell;
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
