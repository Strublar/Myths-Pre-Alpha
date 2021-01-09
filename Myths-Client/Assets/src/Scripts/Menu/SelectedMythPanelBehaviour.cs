using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedMythPanelBehaviour : MonoBehaviour
{
    #region Attributes
    private Myth linkedMyth;

    //Children objects

    public Text nameTag, energyTag, attackTag, rangeTag, addToTeamTag;
    public Image attackBg;
    public HistorySpellBehaviour[] spells, ult;
    public PassiveBehaviour passive;
    public Image addToTeamButton;
    public SelectedTeamBehaviour team;
    #endregion

    #region Getters & Setters
    public Myth LinkedMyth { get => linkedMyth; set => linkedMyth = value; }
    #endregion


    #region Unity Methods

    #endregion

    #region Display Methods

    

    public void InitPanel(Myth myth)
    {
        linkedMyth = myth;
        UpdatePanel();
    }

    public void UpdatePanel()
    {
        if(linkedMyth != null)
        {
            UpdateName();
            UpdateSpells();
            UpdatePassive();
            UpdateAttackAndRange();
            UpdateTeambuilderAction();
        }
        
    }

    public void UpdateEnergy()
    {

        energyTag.text = linkedMyth.Stats[Stat.energy].ToString();
    }

    public void UpdateName()
    {
        nameTag.text = linkedMyth.Name;
    }


    public void UpdateTeambuilderAction()
    {
        if(Server.currentUserMode == UserMode.Teambuilding)
        {
            addToTeamTag.text = "Ajouter";
            foreach (TeambuilderMythBehaviour mythBehaviour in team.myths)
            {
                if (mythBehaviour.LinkedMyth != null)
                {
                    if (mythBehaviour.LinkedMyth.Name == linkedMyth.Name)
                    {
                        addToTeamTag.text = "Retirer";
                    }
                }

            }
        }



        
    }


    public void UpdateSpells()
    {
        for (int i = 0; i < 4; i++)
        {

            spells[i].Init(linkedMyth.Spells[i]);
        }
        ult[0].Init(linkedMyth.Spells[4]);
        ult[1].Init(linkedMyth.Spells[5]);
        ult[2].Init(linkedMyth.Spells[6]);
    }

    public void UpdatePassive()
    {
        passive.InitMyth(linkedMyth);
    }

    public void UpdateAttackAndRange()
    {
        attackTag.text = linkedMyth.Stats[Stat.attack].ToString();
        rangeTag.text = linkedMyth.Stats[Stat.range].ToString();
        attackBg.color = (linkedMyth.Stats[Stat.attackType] == 1) ?
            new Color32(255, 255, 0, 255)
            : new Color32(200, 110, 255, 255);
    }
    #endregion

    #region Control Methods

    public void EnterTeambuilding()
    {
        addToTeamButton.gameObject.SetActive(true);
    }

    public void ExitTeambuilding()
    {
        addToTeamButton.gameObject.SetActive(false);
    }

    public void AddToTeamButtonPressed()
    {

        bool breaker = true;
        //Remove
        foreach (TeambuilderMythBehaviour mythBehaviour in team.myths)
        {
            if(mythBehaviour.LinkedMyth != null)
            {
                if (mythBehaviour.LinkedMyth.Name == linkedMyth.Name)
                {
                    mythBehaviour.RemoveMyth();
                    breaker = false;
                }
            }
            
        }
        if(breaker)
        {
            team.AddMyth(linkedMyth);
        }
        UpdatePanel();
        MenuManager.menuManager.teambuilder.UpdateLeaveButton();
    }
    #endregion
}
