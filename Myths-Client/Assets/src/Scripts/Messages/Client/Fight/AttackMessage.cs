using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AttackMessage : ClientMessage
{
    private int attackerId;
    private int targetId;
    public AttackMessage(int attackerId,int targetId)
    {
        this.messageType = ClientMessageType.Attack;
        this.attackerId = attackerId;
        this.targetId = targetId;
    }

    public override byte[] GetBytes()
    {
        byte[] returnArray = InitMessage();
        returnArray = Message.AddByte(this, returnArray, (byte)messageType);
        returnArray = Message.AddInt(this, returnArray, attackerId);
        returnArray = Message.AddInt(this, returnArray, targetId);
        return returnArray;
    }
}
