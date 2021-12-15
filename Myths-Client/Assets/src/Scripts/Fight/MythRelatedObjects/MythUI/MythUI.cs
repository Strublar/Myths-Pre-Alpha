using Myths_Library;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MythUI : MonoBehaviour
{
    #region Attributes

    private Myth linkedMyth = null;
    //Children objects

    public TextMeshProUGUI hpTag, armorTag, manaTag;
    public GameObject portrait,backGround, toolTip, deadCross;
    public Color normalColor, calledColor, engagedColor, calledEngageColor, deadColor;

    private bool isMouseOver;
    private float timer;
    #endregion

    #region Getters & Setters
    public Myth LinkedMyth { get => linkedMyth; set => linkedMyth = value; }
    #endregion


    #region Unity Methods

    public void Awake()
    {
        isMouseOver = false;
        timer = 0;
        //toolTip.SetActive(false);
    }
    public void Update()
    {
        if (isMouseOver)
        {
            if (timer > 0.5f)
            {
                //ShowToolTip();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
    #endregion

    #region Display Methods
    public void Init(Myth myth)
    {
        linkedMyth = myth;
        UpdateMyth();
    }

    public void UpdateMyth()
    {
        if(linkedMyth != null)
        {
            UpdatePortrait();
            UpdateHp();
            UpdateArmor();
            UpdateMana();
            UpdateState();
            
        }
        
    }


    public void UpdateMana()
    {
        manaTag.text = linkedMyth.Definition.mana.ToString();
    }
    private void UpdateArmor()
    {
        armorTag.text = linkedMyth.Stats[Stat.armor]+" / "+linkedMyth.Definition.armor;
    }

    private void UpdateHp()
    {
        hpTag.text = linkedMyth.Stats[Stat.hp]+" / "+linkedMyth.Definition.hp;
    }


    public void UpdateState()
    {
        if (linkedMyth.Stats[Stat.isDead] == 1)
        {
            backGround.GetComponent<Image>().color = deadColor;
            deadCross.SetActive(true);
        }
        else if (linkedMyth.Stats[Stat.isCalled] == 1 && linkedMyth.Stats[Stat.isEngaged] == 1)
        {
            backGround.GetComponent<Image>().color = calledEngageColor;
        }
        else if (linkedMyth.Stats[Stat.isEngaged] == 1 && linkedMyth.Stats[Stat.isCalled] == 0)
        {
            backGround.GetComponent<Image>().color = engagedColor;
        }
        else if (linkedMyth.Stats[Stat.isEngaged] == 0 && linkedMyth.Stats[Stat.isCalled] == 1)
        {
            backGround.GetComponent<Image>().color = calledColor;
        }
        else
        {
            backGround.GetComponent<Image>().color = normalColor;
        }
    }

    public void UpdatePortrait()
    {
        Sprite portraitTexture = Resources.Load<Sprite>("Data/Portraits/" + linkedMyth.Definition.id);

        portrait.GetComponent<Image>().sprite = portraitTexture;

    }


    #endregion

    #region Display Methods
    


    public void UpdateTooltip()
    {
        string text = linkedMyth.Name
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
            linkedMyth.Stats[Stat.isDead] == 0)
        {
            //First column
            GameManager.gm.selectedUnit = linkedMyth;
            for (int i = 0;i<7;i++)
            {
                for(int j =0;j<2;j++)
                {
                    Tile tile = Tile.board[10 * (GameManager.gm.localPlayerId * (7)+j) + i];
                    if (Utils.UnitOnTile(tile.x, tile.y) == null)
                    {
                        tile.UpdateCallingTexture();
                    }
                }
                
            }

            foreach(Tile tile in Tile.board.Values)
            {
                Unit unitOnTile = Utils.UnitOnTile(tile.x, tile.y);
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
        if (GameManager.gm.selectedTile != null)
        {
            if (GameManager.gm.selectedTile.material.color == GameManager.gm.selectedTile.callColor)
            {
                Unit unitOnTile = Utils.UnitOnTile(GameManager.gm.selectedTile.x, GameManager.gm.selectedTile.y);
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
                    Server.SendMessageToServer(new UnCallMessage(unitOnTile.Id,
                    GameManager.gm.players[GameManager.gm.localPlayerId].Id));

                    Server.SendMessageToServer(new CallMessage(GameManager.gm.selectedUnit.Id,
                    GameManager.gm.players[GameManager.gm.localPlayerId].Id,
                    GameManager.gm.selectedTile.x, GameManager.gm.selectedTile.y,true));
                }
                else if(unitOnTile == null)
                {
                    Debug.Log("Calling");

                    Server.SendMessageToServer(new CallMessage(GameManager.gm.selectedUnit.Id,
                        GameManager.gm.players[GameManager.gm.localPlayerId].Id,
                        GameManager.gm.selectedTile.x, GameManager.gm.selectedTile.y,false));
                }
                
                
            }
        }
        Tile.ResetTiles();
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
