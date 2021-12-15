using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    public class JoinQueueMessage : Message
    {
        public TeamSet teamSet;
        public JoinQueueMessage()
        {
            this.messageType = (byte)ClientMessageType.JoinQueue;
        }

        public JoinQueueMessage(TeamSet teamSet)
        {
            this.messageType = (byte)ClientMessageType.JoinQueue;
            this.teamSet = teamSet;
        }

        public override byte[] GetBytes()
        {
            byte[] returnArray = InitMessage();
            returnArray = Message.AddByte(this, returnArray, (byte)messageType);
            for(int i=0;i<5;i++)
            {
                MythSet currentSet = teamSet.myths[i];
                returnArray = Message.AddInt(this, returnArray, currentSet.id);
                returnArray = Message.AddByte(this, returnArray, currentSet.passive);
                for (int j=0;j<3;j++)
                {
                    returnArray = Message.AddByte(this, returnArray, currentSet.spells[j]);
                }

            }
            return returnArray;
        }

        public override void ParseMessage(byte[] message)
        {
            messageType = ParseByte(message);
            this.teamSet = new TeamSet();
            List<MythSet> mythSets = new List<MythSet>();
            for(int i=0;i<5;i++)
            {
                MythSet currentSet = new MythSet();
                currentSet.id = ParseInt(message);
                currentSet.passive = ParseByte(message);
                for(int j=0;j<3;j++)
                {
                    currentSet.spells[j] = ParseByte(message);
                }
                mythSets.Add(currentSet);
            }
            teamSet.myths = mythSets.ToArray();

        }
    }
}


