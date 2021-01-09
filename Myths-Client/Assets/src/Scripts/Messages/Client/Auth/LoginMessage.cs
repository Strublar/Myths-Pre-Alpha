using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LoginMessage : ClientMessage
{
    string username;
    public LoginMessage(string username)
    {
        this.messageType = ClientMessageType.Login;
        this.username = username;
    }

    public override byte[] GetBytes()
    {
        byte[] returnArray = InitMessage();
        returnArray = Message.AddByte(this, returnArray, (byte)messageType);
        returnArray = Message.AddString(this, returnArray, username);
        return returnArray;

    }
}
