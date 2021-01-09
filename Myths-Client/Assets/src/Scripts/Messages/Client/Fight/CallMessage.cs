using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CallMessage : ClientMessage
{
    private int targetId, playerId;
    private int x, y;
    public CallMessage(int targetId,int playerId,int x, int y)
    {
        this.messageType = ClientMessageType.Call;
        this.targetId = targetId;
        this.playerId = playerId;
        this.x = x;
        this.y = y;
    }

    public override byte[] GetBytes()
    {
        byte[] returnArray = InitMessage();
        returnArray = Message.AddByte(this, returnArray, (byte)messageType);
        returnArray = Message.AddInt(this, returnArray, targetId);
        returnArray = Message.AddInt(this, returnArray, playerId);
        returnArray = Message.AddInt(this, returnArray, x);
        returnArray = Message.AddInt(this, returnArray, y);
        return returnArray;
    }
}
