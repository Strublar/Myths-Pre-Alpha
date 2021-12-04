using Myths_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class Passive 
{
    [Header("Infos")]
    public string passiveName;
    public int id;
    
    public Sprite icon;
    public string description;

    [Header("Triggers")]
    public Trigger[] executionTriggers;
    [Header("Effect")]
    public EffectsGroup[] effects;

    public string ToJson()
    {
        ListeningEffectDefinition definition = BuildDefinition();


        return JsonUtility.ToJson(definition);
    }

    public ListeningEffectDefinition BuildDefinition()
    {
        ListeningEffectDefinition definition = new ListeningEffectDefinition
        {
            id = id,
            name = passiveName,
            description = description
        };
        if (icon != null)
            definition.icon = icon.name;

        //Exec Triggers
        List<TriggerDefinition> executionTriggersList = new List<TriggerDefinition>();
        foreach (Trigger trigger in executionTriggers)
        {
            executionTriggersList.Add(trigger.BuildDefinition());
        }
        definition.executionTriggers = executionTriggersList.ToArray();

        //End Triggers
        definition.endTriggers = new TriggerDefinition[0];

        //Effects Groups
        List<EffectsGroupDefinition> effectsList = new List<EffectsGroupDefinition>();
        foreach (EffectsGroup effectGroup in effects)
        {
            effectsList.Add(effectGroup.BuildDefinition());
        }
        definition.effects = effectsList.ToArray();

        return definition;
    }
}



