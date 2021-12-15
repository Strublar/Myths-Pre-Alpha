using Myths_Library;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/**
 * Tile
 * Script attached to the board tiles in the fight scene
 */
public class Tile : MonoBehaviour
{

    #region Variables
    public static Dictionary<int, Tile> board;


    public int x, y;

    [HideInInspector]public Material material;
    public Color baseColor, moveColor, castColor, callColor;

    public Unit linkedUnit;

    public GameObject selectedTileObject;


    [HideInInspector] public float timer;
    [HideInInspector]public bool isMouseOver = false;

    #endregion


    #region Unity methods
    private void Start()
    {
        material = GetComponent<Renderer>().material;
        try
        {
            board.Add(10*x+y,this);
        }
        catch(Exception)
        {
            Tile.board = new Dictionary<int, Tile> {{ 10*x+y, this } };
        }
    }

    // Update is called once per frame
    void Update()
    {

        //TODO TO BE OPTIMIZED
        if (Input.GetMouseButtonUp(1))
        {
            OnRightMouseUp();
        }
        if(isMouseOver)
        {
            if(timer >= 0.5f)
            {
                ShowPassiveToolTip();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }

    }
    private void OnMouseOver()
    {
        if (GameManager.gm.gameStarted)
        {
            GameManager.gm.selectedTile = this;
            //GetComponent<Renderer>().material.mainTexture = textureTileSelected;
            selectedTileObject.SetActive(true);
            Unit unitOnTile = Utils.UnitOnTile(x, y);
            if(unitOnTile != null)
            {
                ShowUnitTooltip(unitOnTile);
                isMouseOver = true;
            }

        }
        if (Input.GetMouseButtonDown(1)) 
        {
            OnRightMouseDown();
        }
        

        
    }

    private void OnMouseExit()
    {
        if(GameManager.gm.gameStarted)
        {
            if (GameManager.gm.selectedTile == this)
            {
                GameManager.gm.selectedTile = null;
                //GetComponent<Renderer>().material.mainTexture = currentTexture;
                selectedTileObject.SetActive(false);
                HideUnitTooltip();
                isMouseOver = false;
                HidePassiveTooltip();
                timer = 0;
            }
        }
        
    }

    private void OnMouseDown()
    {
        Unit unitOnTile = Utils.UnitOnTile(x, y);
        if (unitOnTile != null)
        {
            if(unitOnTile.OwnerId == GameManager.gm.localPlayerId)
            {
                GameManager.gm.selectedUnit = unitOnTile;
                Tile.UpdateMovingTiles(unitOnTile);
            }
            
        }
        
    }

    private void OnMouseUp()
    {

        if(GameManager.gm.selectedTile != null)
        {
            if (GameManager.gm.selectedTile.material.color == moveColor)
            {

                Unit unitOnTile = Utils.UnitOnTile(GameManager.gm.selectedTile.x,
                    GameManager.gm.selectedTile.y);
                if (unitOnTile == null)
                {
                    if (GameManager.gm.selectedUnit.Stats[Stat.canMove] > 0)
                    {
                        Debug.Log("Moving");
                        Server.SendMessageToServer(new MoveMessage(GameManager.gm.selectedUnit.Id,
                            GameManager.gm.selectedTile.x, GameManager.gm.selectedTile.y));
                    }

                    //DOING
                    /*GameManager.gm.MoveUnit(GameManager.gm.selectedUnit,
                    GameManager.gm.selectedTile.x, GameManager.gm.selectedTile.y);*/
                }
            }
        }
        
        Tile.ResetTiles();
    }



    private void OnRightMouseDown()
    {
        Tile.ResetTiles();
        Unit unitOnTile = Utils.UnitOnTile(x, y);
        if (unitOnTile != null)
        {
            if (unitOnTile.OwnerId == GameManager.gm.localPlayerId)
            {
                GameManager.gm.selectedUnit = unitOnTile;
                Tile.UpdateMovingTiles(unitOnTile);
            }
                
        }
    }

    private void OnRightMouseUp()
    {
        if (GameManager.gm.selectedTile != null)
        {
            if (GameManager.gm.selectedTile.material.color == moveColor)
            {
                Tile selectedTile = GameManager.gm.selectedTile;
                Unit unitOnTile = Utils.UnitOnTile(selectedTile.x, selectedTile.y);
                if ( unitOnTile!= null && 
                    unitOnTile != GameManager.gm.selectedUnit )
                    
                {
                    if(GameManager.gm.selectedUnit.Stats[Stat.canMove] > 0)
                    {
                        Debug.Log("Moving");
                        Server.SendMessageToServer(new MoveMessage(GameManager.gm.selectedUnit.Id,
                            GameManager.gm.selectedTile.x, GameManager.gm.selectedTile.y));

                    }
                    
                    //GameManager.gm.Attack(GameManager.gm.selectedUnit);
                }
            }
        }
        Tile.ResetTiles();
    }
    #endregion

    


    #region Display Methods
    public void ResetTexture()
    {
        material.color = baseColor;
        
    }

    public void UpdateMovingTexture()
    {
        material.color = moveColor;

    }


    public void UpdateCallingTexture()
    {
        material.color = callColor;

    }



    public void UpdateCastingTexture()
    {
        material.color = castColor;
    }

    public void ShowUnitTooltip(Unit selectedUnit)
    {
       /* StatToolTip.SetActive(true);
        hpTag.text = selectedUnit.Stats[Stat.hp].ToString();
        armorTag.text = selectedUnit.Stats[Stat.armor].ToString();
        barrierTag.text = selectedUnit.Stats[Stat.barrier].ToString();
        attackTag.text = selectedUnit.Stats[Stat.attack].ToString();
        masteries[0].sprite = masteryTextures[selectedUnit.Stats[Stat.mastery1]];
        masteries[1].sprite = masteryTextures[selectedUnit.Stats[Stat.mastery2]];
        masteries[2].sprite = masteryTextures[selectedUnit.Stats[Stat.mastery3]];
        attackBg.color = (selectedUnit.Stats[Stat.attackType] == 1) ? 
            new Color32(255, 255, 0, 255) 
            : new Color32(200, 110, 255, 255);*/
    }

    
    public void HideUnitTooltip()
    {
        //StatToolTip.SetActive(false);
    }

    public void UpdateBubble(int damage, int stat)
    {
        //damageBubbles[stat].Init(damage);
    }

    public void ShowPassiveToolTip()
    {
        /*Unit unitOnTile = GameManager.gm.UnitOnTile(x, y);
        if ( unitOnTile!= null)
        {
            toolTipText.text = unitOnTile.Name +
                "\nAppel : " + unitOnTile.Passives[0].Description +
                "\nPassif : " + unitOnTile.Passives[1].Description;
            passiveToolTip.SetActive(true);
        }*/
    }

    public void HidePassiveTooltip()
    {
       // passiveToolTip.SetActive(false);
    }


    #endregion

    #region Display Call Methods
    public static void ResetTiles()
    {
        foreach(Tile tile in board.Values)
        {
            tile.ResetTexture();
        }
    }

    public static void UpdateMovingTiles(Unit unit)
    {
        List<Tile> explored = new List<Tile>{
            board[unit.Stats[Stat.x]*10+unit.Stats[Stat.y]] 
        };

        for (int i=0;i<=unit.Stats[Stat.mobility];i++)
        {
            List<Tile> newExplore = new List<Tile>() ;
            foreach(Tile tile in explored)
            {
                tile.UpdateMovingTexture();
                //Expand all 4 neighbots
                //North
                if(Utils.UnitOnTile(tile.x,tile.y-1)==null)
                {
                    try
                    {
                        newExplore.Add(board[(tile.x)*10+ (tile.y-1) ]);
                    }
                    catch(Exception)
                    {
                        //Out of arena
                    }
                    
                }
                //South
                if (Utils.UnitOnTile(tile.x, tile.y + 1) == null)
                {
                    try
                    {
                        newExplore.Add(board[(tile.x) * 10 + (tile.y + 1)]);
                    }
                    catch (Exception)
                    {
                        //Out of arena
                    }

                }
                //East
                if (Utils.UnitOnTile(tile.x+1, tile.y) == null)
                {
                    try
                    {
                        newExplore.Add(board[(tile.x+1) * 10 + (tile.y)]);
                    }
                    catch (Exception)
                    {
                        //Out of arena
                    }

                }
                //West
                if (Utils.UnitOnTile(tile.x-1, tile.y) == null)
                {
                    try
                    {
                        newExplore.Add(board[(tile.x-1) * 10 + (tile.y)]);
                    }
                    catch (Exception)
                    {
                        //Out of arena
                    }

                }


            }
            explored = newExplore;
        }
    }


    

    public static void UpdateCastingTiles(Unit caster,Spell spell)
    {
        foreach (Tile tile in board.Values)
        {
            int distance = FightDefines.GetDistance(caster.Stats[Stat.x], caster.Stats[Stat.y],
                tile.x, tile.y);
            if (distance >= spell.MinRange && distance <= spell.MaxRange)
            {
                if(CheckCastingShape(caster,spell,tile) && 
                    CheckCastingLos(caster,spell,tile) && 
                    CheckCastingTile(caster, spell, tile))
                {
                    tile.UpdateCastingTexture();
                }
            }
        }

        
    }
    public static bool CheckCastingShape(Unit caster, Spell spell, Tile tile)
    {
        if (spell.Shape.Contains("Normal"))
        {
            return true;
        }
        else if (spell.Shape.Contains("Line") && (tile.x == caster.Stats[Stat.x] || tile.y == caster.Stats[Stat.y]))
        {
            return true;
        }
        return false;
    }

    public static bool CheckCastingLos(Unit caster, Spell spell, Tile tile)
    {
        if (Utils.LineOfSight3(caster.Stats[Stat.x], caster.Stats[Stat.y],
                tile.x, tile.y) || spell.Shape.Contains("NoLOS"))
        {
            return true;
        }
        return false;
    }

    public static bool CheckCastingTile(Unit caster, Spell spell, Tile tile)
    {
        if(spell.Shape.Contains("Tile") && Utils.UnitOnTile(tile.x,tile.y) != null)
        {
            return false;
        }


        return true;
    }
    #endregion



    #region Static Methods

    public static void InitLocalPlayer(int playerId)
    {

        /*foreach (Tile tile in board.Values)
        {
            tile.StatToolTip.transform.localPosition =  new Vector3(0, 10, -4+8*playerId);
            tile.StatToolTip.transform.localEulerAngles = new Vector3(70, -180 * playerId, 0);

            tile.damageBubbles[0].gameObject.transform.localPosition = new Vector3(0, 15, 1.5f - 3 * playerId);
            tile.damageBubbles[0].gameObject.transform.localEulerAngles = new Vector3(70, -180 * playerId, 0);
            tile.damageBubbles[1].gameObject.transform.localPosition = new Vector3(-2+4*playerId, 8, 1.5f - 3 * playerId);
            tile.damageBubbles[1].gameObject.transform.localEulerAngles = new Vector3(70, -180 * playerId, 0);
            tile.damageBubbles[2].gameObject.transform.localPosition = new Vector3(2-4*playerId, 8, 1.5f - 3 * playerId);
            tile.damageBubbles[2].gameObject.transform.localEulerAngles = new Vector3(70, -180 * playerId, 0);
            tile.passiveToolTip.transform.localPosition = new Vector3(0, 8, 5 - 10 * playerId);
            tile.passiveToolTip.transform.localEulerAngles = new Vector3(70, -180 * playerId, 0);
        }*/
    }
    #endregion
}
