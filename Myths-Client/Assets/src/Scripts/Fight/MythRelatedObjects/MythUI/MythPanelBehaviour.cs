using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MythPanelBehaviour : MonoBehaviour
{
    #region Attributes
    private Myth linkedMyth;

    //Children objects

    public UnityEngine.UI.Text nameTag, energyTag;
    public SpellBehaviour[] spells, ults;
    public PassiveBehaviour passive;
    public UnityEngine.UI.Image recallBg;
    public GameObject canAttack, canMove, recallButton;
    #endregion

    #region Getters & Setters
    public Myth LinkedMyth { get => linkedMyth; set => linkedMyth = value; }
    #endregion


    #region Unity Methods
    
    #endregion

    #region Display Methods




    #endregion

    #region Control Methods

    public void InitPanel(Myth myth)
    {
        linkedMyth = myth;
        UpdatePanel();
    }

    public void UpdatePanel()
    {
        UpdateEnergy();
        UpdateName();
        UpdateSpells();
        UpdateIsEngaged();
        UpdateActions();
        UpdatePassive();
    }

    public void UpdateEnergy()
    {

        energyTag.text = linkedMyth.Stats[Stat.energy].ToString();
    }

    public void UpdateName()
    {
        nameTag.text = linkedMyth.Name;
    }

    public void UpdateIsEngaged()
    {
        ults[0].gameObject.SetActive(false);
        ults[1].gameObject.SetActive(false);
        ults[2].gameObject.SetActive(false);
        if (linkedMyth.Stats[Stat.isEngaged]==0)
        {
            recallButton.SetActive(true);
            if (linkedMyth.Stats[Stat.canRecall] == 0)
            {
                recallBg.color = new Color32(170, 170, 170, 255);
            }
            else
            {
                recallBg.color = new Color32(119, 255, 255, 255);
            }
        }
        else
        {
            recallButton.SetActive(false);
            if(linkedMyth.Stats[Stat.canUlt] == 1)
            {
                ults[0].gameObject.SetActive(true);
                ults[1].gameObject.SetActive(true);
                ults[2].gameObject.SetActive(true);
                ults[0].Init(linkedMyth.Spells[4]);
                ults[1].Init(linkedMyth.Spells[5]);
                ults[2].Init(linkedMyth.Spells[6]);
            }
            else
            {
                ults[0].gameObject.SetActive(false);
                ults[1].gameObject.SetActive(false);
                ults[2].gameObject.SetActive(false);
            }
            
        }
        
    }

    public void UpdateActions()
    {
        if(linkedMyth.Stats[Stat.canMove] == 1)
        {
            canMove.SetActive(true);
        }
        else
        {
            canMove.SetActive(false);
        }

        if (linkedMyth.Stats[Stat.canAttack] == 1)
        {
            canAttack.SetActive(true);
        }
        else
        {
            canAttack.SetActive(false);
        }
    }
    
    public void UpdateSpells()
    {
        for(int i=0;i<4;i++)
        {
            
            spells[i].Init(linkedMyth.Spells[i]);
        }
    }

    public void UpdatePassive()
    {
        passive.InitMyth(linkedMyth);
    }

    public void RecallMyth()
    {
        //GameManager.gm.UnCall(linkedMyth);

        //this.gameObject.SetActive(false);
        Debug.Log("Recalling");
        Server.SendMessageToServer(new RecallMessage(linkedMyth.Id, GameManager.gm.players[GameManager.gm.localPlayerId].Id));
    }

    public void MythRecalled()
    {
        this.gameObject.SetActive(false);
    }
    #endregion
}
