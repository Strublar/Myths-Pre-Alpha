using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    public class Message
    {
        private int index;
        protected byte messageType;

        public Message()
        {

        }

        public virtual byte[] GetBytes()
        {
            return null;
        }

        public virtual void ParseMessage(byte[] buffer)
        {

        }
        public byte[] InitMessage()
        {
            this.index = 0;
            byte[] newArray = new byte[256];
            for (int i = 0; i < newArray.Length; i++)
            {
                newArray[i] = (byte)0;
            }
            return newArray;
        }

        #region Edition Static Methods
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
            char[] forbiddenChar = { '\n', '\r', '\0' };
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
            origin.index += byteArray.Length;
            return newArray;
        }
        #endregion

        #region Parsing Methods

        public int ParseInt(byte[] buffer)
        {
            byte[] intArray = new byte[4];
            Array.Copy(buffer, index, intArray, 0, 4);

            index += 4;
            return BitConverter.ToInt32(intArray, 0);
        }
        public byte ParseByte(byte[] buffer)
        {
            index ++;
            return buffer[index - 1];
        }

        public bool ParseBool(byte[] buffer)
        {
            index++;
            return buffer[index - 1] == 1;
        }


        #endregion


    }
    /*    public class TestMessage1 : Message
        {
            public TestMessage1()
            {
                test = "I am 1";
            }
        }
        public class TestMessage2 : Message
        {
            public TestMessage2()
            {
                test = "I am 2";
            }
        }*/

}
