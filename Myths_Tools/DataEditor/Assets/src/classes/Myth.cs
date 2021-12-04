using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Myths_Library;
[CreateAssetMenu(fileName = "NewMyth", menuName = "ScriptableObjects/Myth", order = 1)]
public class Myth  : ScriptableObject
{
    public static int currentId;
    [Header("Infos")]
    [HideInInspector]public int id;
    public string mythName;

    [Header("Stats")]
    public Mastery[] elements;
    public int mana;
    public int armor;
    public int hp;
    public int mobility = 3;
    public int energy = 5;

    [Header("Passives")]
    //public Passive[] masteryPassives;

    public Passive[] passives;
    //public List<Passive> passives;
    [Header("Spells")]
    
    public Spell[] spells;
    public Spell[] masterySpells;

    [Header("Ultimates")]
    public Spell[] ultimates;

    public string ToJson()
    {

        MythDefinition definition = BuildDefinition();

        
        return JsonUtility.ToJson(definition);

    }

    public MythDefinition BuildDefinition()
    {
        MythDefinition definition = new MythDefinition
        {
            id = id,
            name = mythName,
            elements = elements,

            //Stats
            mana = mana,
            armor = armor,
            hp = hp,
            mobility = mobility,
            energy = energy
            
        };

        //Passives
        List<ListeningEffectDefinition> passiveList = new List<ListeningEffectDefinition>();

        foreach (Passive passive in passives)
        {
            passiveList.Add(passive.BuildDefinition());
        }
        definition.passives = passiveList.ToArray();

        //Spells
        List<SpellDefinition> spellList = new List<SpellDefinition>();

        foreach (Spell spell in spells)
        {
            spellList.Add(spell.BuildDefinition());
        }
        definition.spellbook = spellList.ToArray();

        //Spells
        List<SpellDefinition> masterySpellList = new List<SpellDefinition>();

        foreach (Spell masterySpell in masterySpells)
        {
            masterySpellList.Add(masterySpell.BuildDefinition());
        }
        definition.masterySpellBook = masterySpellList.ToArray();

        //Ultimates
        List<SpellDefinition> ultimatesList = new List<SpellDefinition>();

        foreach (Spell ultimate in ultimates)
        {
            ultimatesList.Add(ultimate.BuildDefinition());
        }
        definition.ultimates = ultimatesList.ToArray();

        return definition;
    }

    public static int GetNextId()
    {
        return currentId++;
    }
}

