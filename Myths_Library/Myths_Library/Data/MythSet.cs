using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    [Serializable]
    public class MythSet
    {
        public int id;
        //Index of the passive chosen
        public byte passive;
        //Index of chosen spells in spellbook
        public byte[] spells = new byte[3];




    }
}
