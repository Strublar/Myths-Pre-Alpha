using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    [Serializable]
    public class TriggerDefinition
    {
        public GameEventType eventType;
        public ConditionDefinition[] conditions;
    }  

}
