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
public class TileBehaviour : MonoBehaviour
{

    #region Variables
    public int x, y;

    public Texture currentTexture;
    public Texture textureTileNormal;
    public Texture textureTileSelected;
    public Texture textureTileSummoning;
    public Texture textureTileAttacking;
    public Texture textureTileMoving;
    public Texture textureTileCasting;

    public Unit linkedUnit;

    public GameObject StatToolTip;
    public TextMeshPro hpTag,attackTag,armorTag,barrierTag;
    public SpriteRenderer attackBg;
    public static Dictionary<int,TileBehaviour> board;
    public SpriteRenderer[] masteries;
    public Sprite[] masteryTextures;
    public DamageBubbleBehaviour[] damageBubbles;
    public GameObject selectedTileObject, allyUnit, enemyUnit;

    public GameObject passiveToolTip;
    public TextMeshPro toolTipText;

    public float timer;
    public bool isMouseOver = false;
    public bool isLocalPortal = false;
    #endregion


    #region Unity methods
    private void Start()
    {
        try
        {
            board.Add(10*x+y,this);
        }
        catch(Exception)
        {
            TileBehaviour.board = new Dictionary<int, TileBehaviour> {{ 10*x+y, this } };
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
        
        if(GameManager.gm.gameStarted)
        {
            GameManager.gm.selectedTile = this;
            //GetComponent<Renderer>().material.mainTexture = textureTileSelected;
            selectedTileObject.SetActive(true);

            Unit unitOnTile = GameManager.gm.UnitOnTile(x, y);
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
        Unit unitOnTile = GameManager.gm.UnitOnTile(x, y);
        if (unitOnTile != null)
        {
            if(unitOnTile.OwnerId == GameManager.gm.localPlayerId)
            {
                GameManager.gm.selectedUnit = unitOnTile;
                TileBehaviour.UpdateMovingTiles(unitOnTile);
            }
            
        }
        
    }

    private void OnMouseUp()
    {

        if(GameManager.gm.selectedTile != null)
        {
            if (GameManager.gm.selectedTile.currentTexture == GameManager.gm.selectedTile.textureTileMoving)
            {

                Unit unitOnTile = GameManager.gm.UnitOnTile(GameManager.gm.selectedTile.x,
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
        
        TileBehaviour.ResetTiles();
    }



    private void OnRightMouseDown()
    {
        TileBehaviour.ResetTiles();
        Unit unitOnTile = GameManager.gm.UnitOnTile(x, y);
        if (unitOnTile != null)
        {
            if (unitOnTile.OwnerId == GameManager.gm.localPlayerId)
            {
                GameManager.gm.selectedUnit = unitOnTile;
                TileBehaviour.UpdateAttackTiles(unitOnTile);
            }
                
        }
    }

    private void OnRightMouseUp()
    {
        if (GameManager.gm.selectedTile != null)
        {
            if (GameManager.gm.selectedTile.currentTexture == GameManager.gm.selectedTile.textureTileAttacking)
            {
                TileBehaviour selectedTile = GameManager.gm.selectedTile;
                Unit unitOnTile = GameManager.gm.UnitOnTile(selectedTile.x, selectedTile.y);
                if ( unitOnTile!= null && 
                    unitOnTile != GameManager.gm.selectedUnit )
                    
                {
                    if(GameManager.gm.selectedUnit.Stats[Stat.canAttack] > 0)
                    {
                        Debug.Log("Attacking");
                        if (Utils.LineOfSight3(GameManager.gm.selectedUnit.Stats[Stat.x], 
                            GameManager.gm.selectedUnit.Stats[Stat.y], selectedTile.x, selectedTile.y))
                        {
                            Debug.Log("On los");
                            Server.SendMessageToServer(new AttackMessage(GameManager.gm.selectedUnit.Id, unitOnTile.Id));
                        }
                        else
                        {
                            Debug.Log("Hors los");
                        }
                            
                    }
                    
                    //GameManager.gm.Attack(GameManager.gm.selectedUnit);
                }
            }
        }
        TileBehaviour.ResetTiles();
    }
    #endregion

    


    #region Display Methods
    public void ResetTexture()
    {
        currentTexture = textureTileNormal;
        GetComponent<Renderer>().material.mainTexture = textureTileNormal;
        
    }

    public void UpdateUnitOnTile()
    {
        Unit unitOnTile = GameManager.gm.UnitOnTile(x, y);
        allyUnit.SetActive(false);
        enemyUnit.SetActive(false);
        if (unitOnTile != null)
        {
            if (unitOnTile.OwnerId == GameManager.gm.localPlayerId)
            {
                allyUnit.SetActive(true);
            }
            else
            {
                enemyUnit.SetActive(true);
            }
        }
    }
    public void UpdateMovingTexture()
    {
        currentTexture = textureTileMoving;
        GetComponent<Renderer>().material.mainTexture = textureTileMoving;

    }


    public void UpdateCallingTexture()
    {
        currentTexture = textureTileSummoning;
        GetComponent<Renderer>().material.mainTexture = textureTileSummoning;

    }

    public void UpdateAttackingTexture()
    {
        currentTexture = textureTileAttacking;
        GetComponent<Renderer>().material.mainTexture = textureTileAttacking;

    }

    public void UpdateCastingTexture()
    {
        currentTexture = textureTileCasting;
        GetComponent<Renderer>().material.mainTexture = textureTileCasting;
    }

    public void ShowUnitTooltip(Unit selectedUnit)
    {
        StatToolTip.SetActive(true);
        hpTag.text = selectedUnit.Stats[Stat.hp].ToString();
        armorTag.text = selectedUnit.Stats[Stat.armor].ToString();
        barrierTag.text = selectedUnit.Stats[Stat.barrier].ToString();
        attackTag.text = selectedUnit.Stats[Stat.attack].ToString();
        masteries[0].sprite = masteryTextures[selectedUnit.Stats[Stat.mastery1]];
        masteries[1].sprite = masteryTextures[selectedUnit.Stats[Stat.mastery2]];
        masteries[2].sprite = masteryTextures[selectedUnit.Stats[Stat.mastery3]];
        attackBg.color = (selectedUnit.Stats[Stat.attackType] == 1) ? 
            new Color32(255, 255, 0, 255) 
            : new Color32(200, 110, 255, 255);
    }

    
    public void HideUnitTooltip()
    {
        StatToolTip.SetActive(false);
    }

    public void UpdateBubble(int damage, int stat)
    {
        damageBubbles[stat].Init(damage);
    }

    public void ShowPassiveToolTip()
    {
        Unit unitOnTile = GameManager.gm.UnitOnTile(x, y);
        if ( unitOnTile!= null)
        {
            toolTipText.text = unitOnTile.Name +
                "\nAppel : " + unitOnTile.Passives[0].Description +
                "\nPassif : " + unitOnTile.Passives[1].Description;
            passiveToolTip.SetActive(true);
        }
    }

    public void HidePassiveTooltip()
    {
        passiveToolTip.SetActive(false);
    }

    public void InitPortal()
    {
        textureTileNormal = Resources.Load<Texture>("Tile Sprites/Special Tiles/Portal/Neutral/Base");
        currentTexture = textureTileNormal;
        textureTileSelected = Resources.Load<Texture>("Tile Sprites/Special Tiles/Portal/Neutral/Selected");
        textureTileAttacking = Resources.Load<Texture>("Tile Sprites/Special Tiles/Portal/Neutral/Attacking");
        textureTileCasting = Resources.Load<Texture>("Tile Sprites/Special Tiles/Portal/Neutral/Summon");
        textureTileMoving = Resources.Load<Texture>("Tile Sprites/Special Tiles/Portal/Neutral/Moving");
        textureTileSummoning = Resources.Load<Texture>("Tile Sprites/Special Tiles/Portal/Neutral/Summon");
        ResetTexture();
    }

    public void CapturePortal(int team)
    {
        string path = (team == GameManager.gm.localPlayerId) ? "Ally" : "Enemy";
        isLocalPortal = (team == GameManager.gm.localPlayerId) ? true : false;
        textureTileNormal = Resources.Load<Texture>("Tile Sprites/Special Tiles/Portal/"+path+"/Base");
        currentTexture = textureTileNormal;
        textureTileSelected = Resources.Load<Texture>("Tile Sprites/Special Tiles/Portal/" + path + "/Selected");
        textureTileAttacking = Resources.Load<Texture>("Tile Sprites/Special Tiles/Portal/" + path + "/Attacking");
        textureTileCasting = Resources.Load<Texture>("Tile Sprites/Special Tiles/Portal/" + path + "/Summon");
        textureTileMoving = Resources.Load<Texture>("Tile Sprites/Special Tiles/Portal/" + path + "/Moving");
        textureTileSummoning = Resources.Load<Texture>("Tile Sprites/Special Tiles/Portal/" + path + "/Summon");

        

        ResetTexture();
    }

    #endregion

    #region Display Call Methods
    public static void ResetTiles()
    {
        foreach(TileBehaviour tile in board.Values)
        {
            tile.ResetTexture();
        }
    }

    public static void UpdateMovingTiles(Unit unit)
    {
        List<TileBehaviour> explored = new List<TileBehaviour>{
            board[unit.Stats[Stat.x]*10+unit.Stats[Stat.y]] 
        };

        for (int i=0;i<=unit.Stats[Stat.mobility];i++)
        {
            List<TileBehaviour> newExplore = new List<TileBehaviour>() ;
            foreach(TileBehaviour tile in explored)
            {
                tile.UpdateMovingTexture();
                //Expand all 4 neighbots
                //North
                if(GameManager.gm.UnitOnTile(tile.x,tile.y-1)==null)
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
                if (GameManager.gm.UnitOnTile(tile.x, tile.y + 1) == null)
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
                if (GameManager.gm.UnitOnTile(tile.x+1, tile.y) == null)
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
                if (GameManager.gm.UnitOnTile(tile.x-1, tile.y) == null)
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


    public static void UpdateAttackTiles(Unit unit)
    {
        foreach (TileBehaviour tile in board.Values)
        {
            if (GameManager.gm.GetDistance(unit.Stats[Stat.x], unit.Stats[Stat.y],
                tile.x,tile.y)<=unit.Stats[Stat.range])
            {
                
                if(Utils.LineOfSight3(unit.Stats[Stat.x], unit.Stats[Stat.y],
                    tile.x,tile.y))
                {
                    tile.UpdateAttackingTexture();
                }
                
                
                
            }
        }
    }

    public static void UpdateCastingTiles(Unit caster,Spell spell)
    {
        foreach (TileBehaviour tile in board.Values)
        {
            int distance = GameManager.gm.GetDistance(caster.Stats[Stat.x], caster.Stats[Stat.y],
                tile.x, tile.y);
            if (distance >= spell.MinRange && distance <= spell.MaxRange)
            {
                if(spell.Shape == "Normal" || tile.x == caster.Stats[Stat.x]  || tile.y == caster.Stats[Stat.y])
                {
                    if (Utils.LineOfSight3(caster.Stats[Stat.x], caster.Stats[Stat.y],
                    tile.x, tile.y))
                    {
                        tile.UpdateCastingTexture();
                    }
                }
                
                
            }
        }
    }


    #endregion
    


    #region Static Methods

    public static void InitLocalPlayer(int playerId)
    {

        foreach (TileBehaviour tile in board.Values)
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
        }
    }
    #endregion
}
