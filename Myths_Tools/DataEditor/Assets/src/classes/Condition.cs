using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Myths_Library;

[Serializable]
public class Condition
{


    public ConditionType conditionType;
    public TargetSelector selector;
    public bool inverse;
    public Stat stat;
    public int value;

    public ConditionDefinition BuildDefinition()
    {
        ConditionDefinition definition = new ConditionDefinition
        {
            type = conditionType,
            inverse = inverse,
            stat = stat,
            value = value
        };
        definition.selector = selector.BuildDefinition();

        return definition;
    }
}

