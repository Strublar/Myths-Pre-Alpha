using Myths_Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Attributes
    public static GameManager gm;

    //Fight attributes
    public bool gameStarted = false;

    public Dictionary<int, Entity> entities;
    public Dictionary<int, Player> players;
    public int localPlayerId = 0;
    public int currentPlayer;
    public Transform cameraTransform;

    //Control attributes
    public Tile selectedTile = null;
    public Unit selectedUnit = null;

    public Spell selectedSpell = null;

    
    public GameObject spellPrefab;
    public TurnCounter turnCounter;
    public History history;
    public Timer timer;
    public GameObject yourTurnObject;

    
    #endregion


    #region UnityMethods

    private void Awake()
    {
        GameManager.gm = this;
        FightProcessor.fightUpdates = new Queue<Action<Message>>();
        FightProcessor.fightUpdatesParam = new Queue<Message>();
        entities = new Dictionary<int, Entity>();
        players = new Dictionary<int, Player>();
    }
    private void Start()
    {
        this.gameStarted = true;

    }

    
    #endregion

    #region Init methods
    public void InitPlayer(Message message)
    {
        InitPlayerMessage mess = message as InitPlayerMessage;
        players.Add(mess.playerId,new Player(mess.playerId, mess.entityId, mess.playerName));
        UIManager.m.InitPlayer(mess.playerId, mess.playerName);
        
        if (mess.isLocalPlayer)
            localPlayerId = mess.playerId;
        if(players.Values.Count == 2)
            InitLocalPlayer(localPlayerId);

        Debug.Log("Player " + players[mess.playerId].Name + "initialized");
    }

    public void InitMyth(Message message)
    {
        InitMythMessage mess = message as InitMythMessage;
        Debug.Log("Initializing Myth playerId = " + mess.playerId);
        Debug.Log("Initializing Myth players[playerId] = " + players[mess.playerId]);
        players[mess.playerId].Team[mess.teamIndex] = new Myth(mess.playerId, mess.entityId, mess.set);

        int index = 5 * Mathf.Abs(mess.playerId - localPlayerId) + mess.teamIndex;

        
        UIManager.m.InitMyth(index, players[mess.playerId].Team[mess.teamIndex]);
        /*if(mess.playerId == localPlayerId)
        {
            panels[mess.teamIndex].InitPanel(players[mess.playerId].Team[mess.teamIndex]);
            panels[mess.teamIndex].UpdatePanel();
        }*/

    }

    public void InitLocalPlayer(int playerId)
    {
        localPlayerId = playerId;
        cameraTransform.position = new Vector3(0, 85, -75+150*playerId);

        cameraTransform.eulerAngles = new Vector3(55, -180 * playerId, 0);


        UIManager.m.InitLocalPlayer();



        Tile.InitLocalPlayer(playerId);
    }
  

    #endregion

    #region Event management methods

    public void Call(Unit unit, int x, int y) 
    {
        
        //Update stats
        unit.Stats[Stat.isCalled] = 1;
        unit.Stats[Stat.canMove] = 1;
        unit.Stats[Stat.x] = x;
        unit.Stats[Stat.y] = y;
        unit.Stats[Stat.canRecall] = 0;

        Debug.Log("Calling " + unit.Name + " in position " + unit.Stats[Stat.x]+" "+ unit.Stats[Stat.y]);

        unit.Model = Instantiate(Resources.Load("Data/Models/"+(unit as Myth).Definition.id),
            new Vector3(-44+11*unit.Stats[Stat.x], 0, 33-11*unit.Stats[Stat.y]), 
            Quaternion.Euler(0,cameraTransform.rotation.eulerAngles.y,0)) as GameObject;


        if(unit.OwnerId != GameManager.gm.localPlayerId)
        {
            unit.Model.transform.localScale = new Vector3(-1, 1, 1);
        }

        UIManager.m.Call(unit as Myth);

        UIManager.m.RefreshInterface();
    }

    public void UnCall(Unit unit) //MESSAGE REACTION METHOD (Death or recall)
    {
        unit.Stats[Stat.isCalled] = 0;
        unit.Stats[Stat.isEngaged] = 1;

        unit.Model.SetActive(false);

        //Update panels
        /*if(unit is Myth myth && myth.OwnerId == GameManager.gm.localPlayerId)
        {
            foreach(MythPanelBehaviour panel in panels)
            {
                if(panel.LinkedMyth == myth)
                {
                    panel.MythRecalled();
                }
            }
        }*/

        UIManager.m.RefreshInterface();
    }

    public void MoveUnit(Unit unit, int x, int y)
    {
        //Update stats
        unit.Stats[Stat.x] = x;
        unit.Stats[Stat.y] = y;

        //Vector3 targetPos = new Vector3(-30 + 10 * unit.Stats[Stat.x], 3, 38 - 10 * unit.Stats[Stat.y] + 4 * localPlayerId);
        Vector3 targetPos = new Vector3(-44 + 11 * unit.Stats[Stat.x], 0, 33 - 11 * unit.Stats[Stat.y]);
        iTween.MoveTo(unit.Model, targetPos, 2);
        //unit.Model.GetComponentInChildren<Animator>().Play("Base Layer.Run", 0, 1);

        UIManager.m.RefreshInterface();

    }



    public void EntityStatChanged(Entity target, Stat stat, int newValue)
    {

        //Bubble 
        /*if(stat == Stat.hp)
        {
            foreach (Tile tile in Tile.board.Values)
            {
                if (UnitOnTile(tile.x, tile.y) == target)
                {
                    tile.UpdateBubble(newValue - target.Stats[stat],0);
                }
            }
        }
        if (stat == Stat.armor)
        {
            foreach (Tile tile in Tile.board.Values)
            {
                if (UnitOnTile(tile.x, tile.y) == target)
                {
                    tile.UpdateBubble(newValue - target.Stats[stat], 1);
                }
            }
        }*/

        //update value
        Debug.Log("Stat changed : " + stat.ToString()+" to "+newValue);
        target.Stats[stat] = newValue;
        UIManager.m.OnEntityStatChanged(stat, newValue);
        UIManager.m.RefreshInterface();
        
    }

    public void BeginTurn(int playerId)
    {
        currentPlayer = playerId;
        //Update stats
        foreach (Entity entity in players[playerId].Team)
        {
            if (entity is Unit unit)
            {
                if (entity.Stats[Stat.isCalled] == 1)
                {
                    entity.Stats[Stat.canMove] = 1;
                    if (entity.Stats[Stat.isEngaged] == 0)
                    {
                        entity.Stats[Stat.canRecall] = 1;
                    }
                }
            }
        }

        if (playerId == localPlayerId)
        {
            yourTurnObject.SetActive(true);
        }

        UIManager.m.BeginTurnUpdate(playerId);

        turnCounter.UpdateCounter();
        turnCounter.turnCounter++;

        timer.ResetTimer();
        UIManager.m.RefreshInterface();

    }

    public void EndGame(int winnerId)
    {
        this.gameStarted = false;
        if (winnerId == players[localPlayerId].Id)
        {
            UIManager.m.Win();
        }
        else
        {
            UIManager.m.Lose();
        }
        UIManager.m.RefreshInterface();
    }
    public void SpellCast(int casterId, int spellId, int x, int y)
    {
        Debug.Log("Spell cast by " + entities[casterId] + " on tile " + x + " " + y);
        Unit caster = (Unit)entities[casterId];
        caster.Model.GetComponentInChildren<Animator>().Play("Base Layer.Attack", 0, 1);

        Instantiate(spellPrefab, new Vector3(-30 + 10 * x, 3, 40 - 10 * y),
            Quaternion.identity);

        history.AddSpell(Spell.ParseSpell(spellId));
        UIManager.m.RefreshInterface();
    }

    #endregion


}
