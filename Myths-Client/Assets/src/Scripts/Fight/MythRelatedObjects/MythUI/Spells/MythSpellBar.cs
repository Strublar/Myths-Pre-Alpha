using Myths_Library;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MythSpellBar : MonoBehaviour
{

    public Myth linkedMyth;
    public List<SpellSlot> spells;
    [SerializeField] private TextMeshProUGUI mythName;

    public void Init(Myth myth)
    {
        linkedMyth = myth;
        mythName.gameObject.SetActive(true);
        mythName.text = myth.Name;

        for(int i=0;i<3;i++)
        {
            spells[i].Init(myth.Spells[i], myth.MasterySpells[i]);
        }
    }

    public void UpdateSpells()
    {
        foreach(SpellSlot spell in spells)
        {
            spell.UpdateSpell(linkedMyth);
        }
    }
}
