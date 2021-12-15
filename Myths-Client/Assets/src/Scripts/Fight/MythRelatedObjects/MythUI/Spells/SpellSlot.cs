using Myths_Library;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour
{
    public SpellUI normalSpellUI, masterySpellUI;

    [HideInInspector]public SpellDefinition normalSpellDefinition, masterySpellDefinition;
    [SerializeField] private GameObject masteryBar;
    [SerializeField] private TextMeshProUGUI masteryText;
    [SerializeField] private MasteryLoading masteryLoading;
    

    public void Init(SpellDefinition normalSpell,SpellDefinition masterySpell)
    {
        normalSpellDefinition = normalSpell;
        masterySpellDefinition = masterySpell;

        normalSpellUI.Init(normalSpell);
        masterySpellUI.Init(masterySpell);

        normalSpellUI.gameObject.SetActive(true);
        masterySpellUI.gameObject.SetActive(false);

        masteryLoading.Init(masterySpell) ;
        
    }

    public void UpdateSpell(Myth myth)
    {
        if(normalSpellDefinition.element != Mastery.none)
        {
            if (GameManager.gm.players[GameManager.gm.localPlayerId].Stats[FightDefines.GetStatFromMastery(normalSpellDefinition.element)] >=
                normalSpellDefinition.masteryCost)
            {
                normalSpellUI.gameObject.SetActive(false);
                masterySpellUI.gameObject.SetActive(true);
                masterySpellUI.UpdateSpell(myth);
            }
            else
            {
                normalSpellUI.gameObject.SetActive(true);
                masterySpellUI.gameObject.SetActive(false);
                normalSpellUI.UpdateSpell(myth);
            }
            UpdateMasteryBar();
        }
        
    }


    public void UpdateMasteryBar()
    {
        if(normalSpellDefinition.element != Mastery.none)
        {
            masteryBar.SetActive(true);
            masteryText.gameObject.SetActive(true);
            Player localPlayer = GameManager.gm.players[GameManager.gm.localPlayerId];
            Stat masteryStat = FightDefines.GetStatFromMastery(normalSpellDefinition.element);
            masteryBar.transform.localScale = new Vector3(Mathf.Clamp01(
                (float)localPlayer.Stats[masteryStat] / (float)normalSpellDefinition.masteryCost),
                1f, 1f);

            masteryBar.GetComponent<Image>().color = UIManager.m.masteryColor[normalSpellDefinition.element];

            masteryText.text = localPlayer.Stats[masteryStat] + "/" + normalSpellDefinition.masteryCost;
        }
        else
        {
            masteryBar.SetActive(false);
            masteryText.gameObject.SetActive(false);
        }
        
    }
}
