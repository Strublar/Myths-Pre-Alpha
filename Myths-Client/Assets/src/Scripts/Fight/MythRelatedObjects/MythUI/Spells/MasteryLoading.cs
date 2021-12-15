using Myths_Library;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasteryLoading : MonoBehaviour
{
    public SpellDefinition linkedSpell;
    public SpellToolTip toolTip;

    [SerializeField] private float tooltipTimer;
    [SerializeField] private bool isInit = false;
    [SerializeField] private float timer;
    [SerializeField] private bool isMouseOver = false;

    public void Init(SpellDefinition spell)
    {
        linkedSpell = spell;
        isInit = true;

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
        Debug.Log("Wololo");
    }

    public void OnPointerExit()
    {
        timer = 0;
        isMouseOver = false;
        toolTip.gameObject.SetActive(false);
        Debug.Log("Wololo2");
    }

    public void ShowToolTip()
    {
        toolTip.gameObject.SetActive(true);
        toolTip.Init(linkedSpell);
    }
    #endregion
}
