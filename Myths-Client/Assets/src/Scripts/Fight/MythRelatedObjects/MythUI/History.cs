using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class History : MonoBehaviour
{
    public HistorySpellBehaviour[] spells;


    public void AddSpell(Spell spell)
    {
        for(int i=5;i>=1;i--)
        {
            spells[i].Init(spells[i - 1].LinkedSpell);
            
        }
        spells[0].Init(spell);
    }
}
