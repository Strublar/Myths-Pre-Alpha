using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EndTurnMessage : ClientMessage
{
    public EndTurnMessage()
    {
        this.messageType = ClientMessageType.EndTurn;

    }

    public override byte[] GetBytes()
    {
        byte[] returnArray = InitMessage();
        returnArray = Message.AddByte(this, returnArray, (byte)messageType);
        return returnArray;
    }
}
