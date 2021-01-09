using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RecallMessage : ClientMessage
{
    private int targetId, playerId;
    public RecallMessage(int targetId, int playerId)
    {
        this.messageType = ClientMessageType.Recall;
        this.targetId = targetId;
        this.playerId = playerId;
    }

    public override byte[] GetBytes()
    {
        byte[] returnArray = InitMessage();
        returnArray = Message.AddByte(this, returnArray, (byte)messageType);
        returnArray = Message.AddInt(this, returnArray, targetId);
        returnArray = Message.AddInt(this, returnArray, playerId);
        return returnArray;
    }
}
