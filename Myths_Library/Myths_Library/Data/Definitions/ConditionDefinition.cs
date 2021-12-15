using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    [Serializable]
    public class ConditionDefinition
    {

        public ConditionType type;
        public TargetSelectorDefinition selector;

        //Data

        public bool inverse;
        public Stat stat;
        public int value;
    }

    public enum ConditionType : byte
    {
        hasStat,
        isSource,
        isTarget,
        isEffectHolder
    }
}
