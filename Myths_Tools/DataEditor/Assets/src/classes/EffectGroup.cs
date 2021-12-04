using Myths_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[Serializable]
public class EffectsGroup
{

    public Effect[] effects;
    public TargetSelector targets;
    public Condition[] conditions;


    public EffectsGroupDefinition BuildDefinition()
    {
        EffectsGroupDefinition definition = new EffectsGroupDefinition();

        //Effects
        List<EffectDefinition> effectsList = new List<EffectDefinition>();
        foreach (Effect effect in effects)
        {
            effectsList.Add(effect.BuildDefinition());
        }
        definition.effects = effectsList.ToArray();

        //TargetSelector
        definition.targets = targets.BuildDefinition();


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

