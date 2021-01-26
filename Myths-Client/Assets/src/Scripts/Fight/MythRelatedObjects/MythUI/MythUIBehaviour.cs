using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MythUIBehaviour : MonoBehaviour
{
    #region Attributes

    private Myth linkedMyth = null;
    //Children objects

    public UnityEngine.UI.Text nameTag;
    public UnityEngine.UI.Text hpTag, armorTag, barrierTag, maxHpTag,maxArmorTag,maxBarrierTag;
    public GameObject portrait,backGround, toolTip, deadCross;
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
        if(linkedMyth != null)
        {
            UpdateName();
            UpdatePortrait();
            UpdateHp();
            UpdateArmor();
            UpdateBarrier();
            UpdateState();
        }
        
    }

    private void UpdateMaxHp()
    {
    }

    private void UpdateBarrier()
    {
        barrierTag.text = linkedMyth.Stats[Stat.barrier].ToString();
    }

    private void UpdateArmor()
    {
        armorTag.text = linkedMyth.Stats[Stat.armor].ToString();
    }

    private void UpdateHp()
    {
        hpTag.text = linkedMyth.Stats[Stat.hp].ToString();
    }

    public void UpdateName()
    {
        nameTag.text = linkedMyth.Name;

    }

    public void UpdateState()
    {
        if (linkedMyth.Stats[Stat.isDead] == 1)
        {
            backGround.GetComponent<Image>().color = new Color32(255, 150, 150, 255);
            deadCross.SetActive(true);
        }
        else if (linkedMyth.Stats[Stat.isCalled] == 1 && linkedMyth.Stats[Stat.isEngaged] == 1)
        {
            backGround.GetComponent<Image>().color = new Color32(0, 166, 0, 255);
        }
        else if (linkedMyth.Stats[Stat.isEngaged] == 1 && linkedMyth.Stats[Stat.isCalled] == 0)
        {
            backGround.GetComponent<Image>().color = new Color32(65, 217, 43, 255);
        }
        else if (linkedMyth.Stats[Stat.isEngaged] == 0 && linkedMyth.Stats[Stat.isCalled] == 1)
        {
            backGround.GetComponent<Image>().color = new Color32(67, 173, 192, 255);
        }
        else
        {
            backGround.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void UpdatePortrait()
    {
        var portraitTexture = Resources.Load<Sprite>("Units/"+linkedMyth.Name+"/Portrait");

        portrait.GetComponent<UnityEngine.UI.Image>().sprite = portraitTexture;


    }


    #endregion

    #region Display Methods
    


    public void UpdateTooltip()
    {
        string text = linkedMyth.Name
            + "\nAttaque :  "+linkedMyth.Stats[Stat.attack]
            + "\nPortee : "+linkedMyth.Stats[Stat.range]
            + "\nAppel : "+linkedMyth.Passives[0].Description
            + "\nPassif : "+linkedMyth.Passives[1].Description;
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

    public void OnDrag()
    {
        Debug.Log("Drag");
        if (linkedMyth.OwnerId == GameManager.gm.localPlayerId &&
            linkedMyth.Stats[Stat.isCalled]==0 &&
            linkedMyth.Stats[Stat.isDead] == 0 &&
            GameManager.gm.players[GameManager.gm.localPlayerId].Stats[Stat.calls]>0)
        {
            //First column
            GameManager.gm.selectedUnit = linkedMyth;
            for (int i = 0;i<5;i++)
            {
                TileBehaviour tile = TileBehaviour.board[10 * (GameManager.gm.localPlayerId * 6) + i];
                if(GameManager.gm.UnitOnTile(tile.x,tile.y)==null)
                {
                    tile.UpdateCallingTexture();
                }
            }

            foreach(TileBehaviour tile in TileBehaviour.board.Values)
            {
                //Portal
                Unit unitOnTile = GameManager.gm.UnitOnTile(tile.x, tile.y);

                if (tile.isLocalPortal && unitOnTile == null)
                {
                    tile.UpdateCallingTexture();
                }
                //Valid for switch

                if (unitOnTile != null)
                {

                    if(unitOnTile is Myth && unitOnTile.OwnerId == GameManager.gm.localPlayerId &&
                        unitOnTile.Stats[Stat.isEngaged] == 0)
                    {
                        int counterCalled = 0;
                        foreach (Entity entity in GameManager.gm.entities.Values)
                        {
                            
                            if (entity is Myth m)
                            {
                                if (m.OwnerId == GameManager.gm.localPlayerId && m.Stats[Stat.isCalled] == 1)
                                {
                                    counterCalled++;
                                }
                            }
                        }
                        if(counterCalled >1)
                        {
                            tile.UpdateCallingTexture();
                        }
                        
                    }
                }
                
            }
        }
    }

    public void OnDrop()
    {
        Debug.Log("WOLOLO");
        if (GameManager.gm.selectedTile != null)
        {
            if (GameManager.gm.selectedTile.currentTexture == GameManager.gm.selectedTile.textureTileSummoning)
            {
                Unit unitOnTile = GameManager.gm.UnitOnTile(GameManager.gm.selectedTile.x, GameManager.gm.selectedTile.y);
                int counterCalled = 0;
                foreach(Entity entity in GameManager.gm.entities.Values)
                {
                    if(entity is Myth m)
                    {
                        if(m.OwnerId == GameManager.gm.localPlayerId && m.Stats[Stat.isCalled] == 1)
                        {
                            counterCalled++;
                        }
                    }
                }
                if (unitOnTile != null && unitOnTile is Myth myth && counterCalled >1)
                {
                    Debug.Log("Switch");
                    Server.SendMessageToServer(new RecallMessage(unitOnTile.Id,
                    GameManager.gm.players[GameManager.gm.localPlayerId].Id));

                    Server.SendMessageToServer(new CallMessage(GameManager.gm.selectedUnit.Id,
                    GameManager.gm.players[GameManager.gm.localPlayerId].Id,
                    GameManager.gm.selectedTile.x, GameManager.gm.selectedTile.y));
                }
                else if(unitOnTile == null)
                {
                    Debug.Log("Calling");

                    Server.SendMessageToServer(new CallMessage(GameManager.gm.selectedUnit.Id,
                        GameManager.gm.players[GameManager.gm.localPlayerId].Id,
                        GameManager.gm.selectedTile.x, GameManager.gm.selectedTile.y));
                }
                
                
            }
        }
        TileBehaviour.ResetTiles();
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
