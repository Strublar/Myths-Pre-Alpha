using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class LeaveQueueMessage : ClientMessage
{
    public LeaveQueueMessage()
    {
        this.messageType = ClientMessageType.LeaveQueue;

    }

    public override byte[] GetBytes()
    {
        byte[] returnArray = InitMessage();
        returnArray = Message.AddByte(this, returnArray, (byte)messageType);
        return returnArray;

    }
}

