using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    [Serializable]
    public class EffectDefinition
    {
        

        public EffectType effectType;

        //Effects data
        public Mastery element;
        public int amount;
        public bool temporary;
        public Stat stat;

    }
    public enum EffectType : byte
    {
        dealDamage,
        dealTrueDamage,
        heal,
        gainArmor,
        gainMastery,
        modifyStat,
        placeListeningEffect,
        push,
        pull,
        swap,
        teleport,
        consumeMastery,
        transferDefenses,

        
    }
}
