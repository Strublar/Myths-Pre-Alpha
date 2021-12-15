using Myths_Library;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellUI : MonoBehaviour
{
    public SpellDefinition linkedSpell;

    public Image icon;
    public SpellToolTip toolTip;

    [SerializeField] private float tooltipTimer;
    private bool isInit = false;
    private float timer;
    private bool isMouseOver = false;

    

    public void Init(SpellDefinition spell)
    {
        isInit = true;
        linkedSpell = spell;
        icon.sprite = Resources.Load<Sprite>("Data/SpellSprites/" + spell.icon);
        icon.gameObject.SetActive(true);
    }

    public void UpdateSpell(Myth myth)
    {
        Debug.Log("SPell " + linkedSpell.name + " cost " + linkedSpell.cost + " vs energy :" + myth.Stats[Stat.energy]);
        if (myth.Stats[Stat.energy] >= linkedSpell.cost)
        {
            icon.color = new Color(1f, 1f, 1f);
        }
        else
        {
            icon.color = new Color(.5f, .5f, .5f);
        }
    }
    #region ToolTip
    public void Update()
    {
        if (isMouseOver && isInit)
        {
            timer += Time.deltaTime;
            if (timer >= tooltipTimer)
                ShowToolTip();
        }
    }
    public void OnPointerEnter()
    {
        timer = 0;
        isMouseOver = true;
    }

    public void OnPointerExit()
    {
        timer = 0;
        isMouseOver = false;
        toolTip.gameObject.SetActive(false);
    }

    public void ShowToolTip()
    {
        toolTip.gameObject.SetActive(true);
        toolTip.Init(linkedSpell);
    }
    #endregion
}
