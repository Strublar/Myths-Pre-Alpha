using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MoveMessage : ClientMessage
{
    private int targetId, x, y;
    public MoveMessage(int targetId,int x, int y)
    {
        this.messageType = ClientMessageType.Move;
        this.targetId = targetId;
        this.x = x;
        this.y = y;
    }

    public override byte[] GetBytes()
    {
        byte[] returnArray = InitMessage();
        returnArray = Message.AddByte(this, returnArray, (byte)messageType);
        returnArray = Message.AddInt(this, returnArray, targetId);
        returnArray = Message.AddInt(this, returnArray, x);
        returnArray = Message.AddInt(this, returnArray, y);
        return returnArray;
    }
}
