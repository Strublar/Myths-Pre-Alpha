using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    public class ConditionBuilder
    {
        public static Dictionary<ConditionType, Type> conditionMap = new Dictionary<ConditionType, Type>()
            {
                {ConditionType.hasStat, typeof(HasStatCondition)},
                {ConditionType.isSource, typeof(IsSourceCondition)},
                {ConditionType.isTarget, typeof(IsTargetCondition)},
                {ConditionType.isEffectHolder, typeof(IsEffectHolderCondition)},

            };



        public static Condition BuildFrom(ConditionDefinition definition)
        {

            Condition newCondition = Activator.CreateInstance(conditionMap[definition.type],definition) as Condition;
            return newCondition;
        }

    }
}
