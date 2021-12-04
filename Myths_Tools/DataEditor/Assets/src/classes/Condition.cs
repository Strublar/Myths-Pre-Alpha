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
    public bool mustBeTrue = true;
    public Stat stat;
    public StatOperation statOperation;
    public int value;

    public ConditionDefinition BuildDefinition()
    {
        ConditionDefinition definition = new ConditionDefinition
        {
            type = conditionType,
            mustBeTrue = mustBeTrue,
            stat = stat,
            statOperation = statOperation,
            value = value
        };

        return definition;
    }
}

