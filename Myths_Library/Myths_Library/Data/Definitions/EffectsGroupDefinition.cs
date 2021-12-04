using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    [Serializable]
    public class EffectsGroupDefinition
    {
        public EffectDefinition[] effects;
        public TargetSelectorDefinition targets;
        public ConditionDefinition[] conditions;
    }
}
