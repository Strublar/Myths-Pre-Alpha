using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveBehaviour : MonoBehaviour
{

    public GameObject toolTip;
    private Myth linkedMyth;
    private float timer;
    private bool isMouseOver = false;
    public Myth LinkedMyth { get => linkedMyth; set => linkedMyth = value; }


    public void Update()
    {
        if(isMouseOver )
        {
            if(timer >= 0.5f)
            {
                ShowToolTip();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        
    }

    public void InitMyth(Myth myth)
    {
        this.linkedMyth = myth;
    }

    public void ShowToolTip()
    {
        UpdateToolTipText();
        toolTip.gameObject.SetActive(true);
    }

    public void UpdateToolTipText()
    {
        string text = linkedMyth.Name
            + "\nAppel : " + linkedMyth.Passives[0].Description
            + "\n\nPassif : " + linkedMyth.Passives[1].Description;
        toolTip.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
    }

    public void HideToolTip()
    {
        toolTip.SetActive(false);
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
        toolTip.SetActive(false);
    }
}
