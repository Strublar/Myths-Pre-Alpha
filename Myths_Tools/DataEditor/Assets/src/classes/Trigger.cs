using Myths_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class Trigger
{
    public GameEventType eventType;
    public Condition[] conditions;

    public TriggerDefinition BuildDefinition()
    {

        TriggerDefinition definition = new TriggerDefinition
        {
            eventType = eventType
        };

        //Conditions
        List<ConditionDefinition> conditionList = new List<ConditionDefinition>();
        foreach (Condition condition in conditions)
        {
            conditionList.Add(condition.BuildDefinition());
        }
        definition.conditions = conditionList.ToArray();

        return definition;
    }
}

