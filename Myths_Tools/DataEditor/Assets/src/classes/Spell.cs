using Myths_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class Spell
{

    [Header("Infos")]
    public string spellName;

    public int id;
    public Mastery element;
    public bool isUltimate;
    public bool isMastery;
    public Sprite icon;
    public string description;
    [Header("Stats")]

    public int cost;
    public int masteryGeneration;
    public int masteryCost;
    public int minRange;
    public int maxRange;

    [Header("Effects")]

    public EffectsGroup[] effects;

    public SpellDefinition BuildDefinition()
    {
        SpellDefinition definition = new SpellDefinition()
        {
            name = spellName,
            id = id,
            element = element,
            isUlt = isUltimate,
            isMastery = isMastery,
            description = description,
            cost = cost,
            masteryGeneration = masteryGeneration,
            masteryCost = masteryCost,
            minRange = minRange,
            maxRange = maxRange
        };
        if (icon != null)
            definition.icon = icon.name;

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

