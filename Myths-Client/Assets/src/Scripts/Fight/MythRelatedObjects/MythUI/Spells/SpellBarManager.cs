using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBarManager : MonoBehaviour
{
    public List<MythSpellBar> spellBars;


    public void AddMyth(Myth myth)
    {
        for(int i =0;i<3;i++)
        {
            if(spellBars[i].linkedMyth == null)
            {
                spellBars[i].Init(myth);
                break;
            }
        }
    }

    public void UpdateSpellBar()
    {
        for (int i = 0; i < 3; i++)
        {
            spellBars[i].UpdateSpells();
        }
    }
}
