using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    public abstract class Message
    {
        public int index = 0;
        public string GetString()
        {
            return Encoding.UTF8.GetString(this.GetBytes());
        }
        public abstract byte[] GetBytes();

        public static byte[] AddInt(Message origin, byte[] message, int integer)
        {
            byte[] newArray = new byte[256];
            Array.Copy(message, newArray, message.Length);
            byte[] integerArray = BitConverter.GetBytes(integer);
            Array.Copy(integerArray, 0, newArray, origin.index, integerArray.Length);
            origin.index += integerArray.Length;
            return newArray;
        }

        public static byte[] AddBool(Message origin, byte[] message, bool boolean)
        {
            byte[] newArray = new byte[256];
            Array.Copy(message, newArray, message.Length);
            byte[] boolArray = { (byte)((boolean) ? 1 : 0) };
            Array.Copy(boolArray, 0, newArray, origin.index, boolArray.Length);
            origin.index += boolArray.Length;
            return newArray;
        }

        public static byte[] AddString(Message origin, byte[] message, string value)
        {
            byte[] newArray = new byte[256];
            char[] forbiddenChar = { '\n', '\r','\0' };
            Array.Copy(message, newArray, message.Length);
            byte[] stringArray = Encoding.UTF8.GetBytes(value.TrimEnd(forbiddenChar));

            Array.Copy(stringArray, 0, newArray, origin.index, stringArray.Length);
            origin.index += stringArray.Length;
            return newArray;
        }

        public static byte[] AddByte(Message origin, byte[] message, byte value)
        {
            
            byte[] newArray = new byte[256];
            Array.Copy(message, newArray, message.Length);
            byte[] byteArray = { value };
            Array.Copy(byteArray, 0, newArray, origin.index, byteArray.Length);
            origin.index+= byteArray.Length;
            return newArray;
        }

        public byte[] InitMessage()
        {
            this.index = 0;
            byte[] newArray = new byte[256];
            for(int i=0;i<newArray.Length;i++)
            {
                newArray[i] = (byte)0;
            }
            return newArray;
        }

    }
}
