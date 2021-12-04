using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    [Serializable]
    public class MythDefinition : EntityDefinition
    {
        public Mastery[] elements;
        //Stats
        public int mana;
        public int hp;
        public int armor;
        public int mobility;
        public int energy;
        
        //public ListeningEffectDefinition[] masteryPassives;
        public SpellDefinition[] spellbook;
        public SpellDefinition[] masterySpellBook;
        public SpellDefinition[] ultimates;

    }
}
