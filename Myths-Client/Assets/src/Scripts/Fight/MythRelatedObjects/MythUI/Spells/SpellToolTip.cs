using Myths_Library;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellToolTip : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI spellName, masteryText, rangeText, spellDescription;
    [SerializeField] private Image masteryBackground;
    [SerializeField] private Sprite lightMastery, darkMastery, fireMastery, waterMastery, earthMastery, airMastery;
    public void Init(SpellDefinition spell)
    {
        spellName.text = spell.name;
        masteryText.text = spell.masteryGeneration.ToString();
        rangeText.text = "Range : " + spell.minRange + "-" + spell.maxRange;
        spellDescription.text = spell.description;

        switch(spell.element)
        {
            case Mastery.light:
                masteryBackground.sprite = lightMastery;
                break;
            case Mastery.dark:
                masteryBackground.sprite = darkMastery;
                break;
            case Mastery.fire:
                masteryBackground.sprite = fireMastery;
                break;
            case Mastery.air:
                masteryBackground.sprite = airMastery;
                break;
            case Mastery.earth:
                masteryBackground.sprite = earthMastery;
                break;
            case Mastery.water:
                masteryBackground.sprite = waterMastery;
                break;
                

        }
    }
}
