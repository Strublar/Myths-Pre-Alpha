using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    [Serializable]
    public class SpellDefinition
    {
        public int id;
        public string name;
        public Mastery element;
        public string description;
        public string icon;
        public bool isUlt;
        public bool isMastery;

        public int masteryGeneration;
        public int masteryCost;
        public int cost;
        public int minRange;
        public int maxRange;

        public EffectsGroupDefinition[] effects;


       

    }

    public enum SpellShape
    {
        normal,
        line,
        dash,
        noLos
    }
}
