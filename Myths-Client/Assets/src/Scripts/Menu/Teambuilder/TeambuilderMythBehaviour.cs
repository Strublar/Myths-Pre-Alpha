using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeambuilderMythBehaviour : MonoBehaviour
{
    #region Attributes
    private Myth linkedMyth = null;
   
    //Parent
    public SelectedTeamBehaviour team;

    //Children objects

    public UnityEngine.UI.Text nameTag;
    public UnityEngine.UI.Text hpTag, armorTag, barrierTag, maxHpTag;
    public GameObject portrait, toolTip;
    public bool isMouseOver;
    public float timer;
    #endregion

    #region Getters & Setters
    public Myth LinkedMyth { get => linkedMyth; set => linkedMyth = value; }
    #endregion


    #region Unity Methods
    public void Awake()
    {
        isMouseOver = false;
        timer = 0;
        toolTip.SetActive(false);
    }
    public void Update()
    {
        if (isMouseOver)
        {
            if (timer > 0.5f)
            {
                ShowToolTip();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
    #endregion

    #region Display Methods
    public void UpdateMyth()
    {
        if (linkedMyth != null)
        {
            UpdateName();
            UpdatePortrait();
            UpdateHp();
            UpdateArmor();
            UpdateBarrier();
        }

    }

    public void UpdateName()
    {
        nameTag.text = linkedMyth.Name;

    }


    public void UpdatePortrait()
    {
        var portraitTexture = Resources.Load<Sprite>("Units/" + linkedMyth.Name + "/Portrait");

        portrait.GetComponent<UnityEngine.UI.Image>().sprite = portraitTexture;


    }

    public void UpdateHp()
    {
        hpTag.text = linkedMyth.Stats[Stat.hp].ToString();
        maxHpTag.text = linkedMyth.Stats[Stat.hp].ToString();
    }

    public void UpdateArmor()
    {
        armorTag.text = linkedMyth.Stats[Stat.armor].ToString();
    }

    public void UpdateBarrier()
    {
        barrierTag.text = linkedMyth.Stats[Stat.barrier].ToString();
    }


    public void RemoveMyth()
    {
        this.LinkedMyth = null;
        hpTag.text = "";
        armorTag.text = "";
        barrierTag.text = "";
        maxHpTag.text = "";
        nameTag.text = "";

        var portraitTexture = Resources.Load<Sprite>("UI Sprites/Myth UI");

        portrait.GetComponent<UnityEngine.UI.Image>().sprite = portraitTexture;
        
    }

    public void UpdateTooltip()
    {
        string text = linkedMyth.Name
            + "\nAttaque :  " + linkedMyth.Stats[Stat.attack]
            + "\nPortee : " + linkedMyth.Stats[Stat.range]
            + "\nAppel : " + linkedMyth.Passives[0].Description
            + "\nPassif : " + linkedMyth.Passives[1].Description;
        toolTip.GetComponentInChildren<Text>().text = text;
    }
    public void ShowToolTip()
    {
        UpdateTooltip();
        toolTip.SetActive(true);
    }

    public void HideToolTip()
    {
        toolTip.SetActive(false);
    }
    #endregion

    #region Control Methods
    public void SelectMyth()
    {
        team.UpdateSelectedMyth(linkedMyth);
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
    #endregion

}
