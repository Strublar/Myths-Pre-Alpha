using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    [Serializable]
    public class ListeningEffectDefinition
    {
        public int id;
        public string name;
        public string description;
        public string icon;

        public TriggerDefinition[] executionTriggers;
        public TriggerDefinition[] endTriggers;
        public EffectsGroupDefinition[] effects;
    }
}
