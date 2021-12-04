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

        public bool mustBeTrue;
        public Stat stat;
        public StatOperation statOperation;
        public int value;
    }

    public enum ConditionType : byte
    {
        statEventCondition,
    }

    public enum StatOperation
    {
        changed,
        gained,
        lost
    }



}
