using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class JoinQueueMessage : ClientMessage
{
    private int mythId1, mythId2, mythId3, mythId4, mythId5;
    public JoinQueueMessage(int id1, int id2, int id3, int id4, int id5)
    {
        this.messageType = ClientMessageType.JoinQueue;
        mythId1 = id1;
        mythId2 = id2;
        mythId3 = id3;
        mythId4 = id4;
        mythId5 = id5;
    }

    public override byte[] GetBytes()
    {
        byte[] returnArray = InitMessage();
        returnArray = Message.AddByte(this, returnArray, (byte)messageType);
        returnArray = Message.AddInt(this, returnArray, mythId1);
        returnArray = Message.AddInt(this, returnArray, mythId2);
        returnArray = Message.AddInt(this, returnArray, mythId3);
        returnArray = Message.AddInt(this, returnArray, mythId4);
        returnArray = Message.AddInt(this, returnArray, mythId5);
        return returnArray;
    }
}

