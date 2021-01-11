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
    public int localPlayerId;
    public int currentPlayer;
    public Transform cameraTransform;

    //Control attributes
    public TileBehaviour selectedTile = null;
    public Unit selectedUnit = null;

    public Spell selectedSpell = null;

    //Linked objects
    public MythUIBehaviour[] mythPortraits;
    public PlayerNameBehaviour[] playerNames;
    public MythPanelBehaviour[] panels;
    public CallCounterBehaviour[] callCounters;
    public GameObject winScreen, loseScreen;
    public GameObject spellPrefab;
    public GaugePanelBehaviour gaugePanel;
    public TurnCounterBehaviour turnCounter;
    public HistoryBehaviour history;
    public TimerBehaviour timer;
    public GameObject yourTurnObject;

    public static Queue<Action<object[]>> fightUpdates;
    public static Queue<object[]> fightUpdatesParam;
    #endregion


    #region UnityMethods

    private void Awake()
    {
        GameManager.gm = this;
        fightUpdates = new Queue<Action<object[]>>();
        fightUpdatesParam = new Queue<object[]>();
        entities = new Dictionary<int, Entity>();
        players = new Dictionary<int, Player>();
    }
    private void Start()
    {
        this.gameStarted = true;

    }

    private void Update()
    {

        while (fightUpdates.Count > 0)
        {
            fightUpdates.Dequeue().Invoke(fightUpdatesParam.Dequeue());
        }


    }
    #endregion


    #region Information Methods

    public Unit UnitOnTile(int x, int y)
    {
        var units = entities.Values.OfType<Unit>();

        Unit unitOnTile = null;
        List<Unit> unitsOnTile = (from unit in units
                          where unit.Stats[Stat.x] == x
                          && unit.Stats[Stat.y] == y &&
                          unit.Stats[Stat.isCalled] == 1
                          select unit).ToList();
        if(unitsOnTile.Count == 1)
        {
            unitOnTile = unitsOnTile[0];
        }
        else if(unitsOnTile.Count > 1)
        {
            throw new System.Exception("There can't be multiple units on this tile : " + x + " " + y);
        }

        return unitOnTile;
    }

    public int GetDistance(int x1, int y1, int x2, int y2)
    {
        return (Mathf.Abs(x2 - x1) + Mathf.Abs(y2 - y1));
    }


    public int GetCostReduction(Mastery element)
    {
        int costReduction = 0;
        switch (element)
        {
            case Mastery.arcane:
                costReduction += GameManager.gm.players[GameManager.gm.localPlayerId].Stats[Stat.gaugeArcane];
                break;
            case Mastery.light:
                costReduction += GameManager.gm.players[GameManager.gm.localPlayerId].Stats[Stat.gaugeArcane];
                costReduction += GameManager.gm.players[GameManager.gm.localPlayerId].Stats[Stat.gaugeLight];
                break;
            case Mastery.dark:
                costReduction += GameManager.gm.players[GameManager.gm.localPlayerId].Stats[Stat.gaugeArcane];
                costReduction += GameManager.gm.players[GameManager.gm.localPlayerId].Stats[Stat.gaugeDark];
                break;
            case Mastery.fire:
                costReduction += GameManager.gm.players[GameManager.gm.localPlayerId].Stats[Stat.gaugeArcane];
                costReduction += GameManager.gm.players[GameManager.gm.localPlayerId].Stats[Stat.gaugeFire];
                break;
            case Mastery.earth:
                costReduction += GameManager.gm.players[GameManager.gm.localPlayerId].Stats[Stat.gaugeArcane];
                costReduction += GameManager.gm.players[GameManager.gm.localPlayerId].Stats[Stat.gaugeEarth];
                break;
            case Mastery.air:
                costReduction += GameManager.gm.players[GameManager.gm.localPlayerId].Stats[Stat.gaugeArcane];
                costReduction += GameManager.gm.players[GameManager.gm.localPlayerId].Stats[Stat.gaugeAir];
                break;
            case Mastery.water:
                costReduction += GameManager.gm.players[GameManager.gm.localPlayerId].Stats[Stat.gaugeArcane];
                costReduction += GameManager.gm.players[GameManager.gm.localPlayerId].Stats[Stat.gaugeWater];
                break;

        }
        return costReduction;
    }
    #endregion

    #region Init methods
    public void InitPlayer(int playerId,int entityId, string playerName, bool isLocalPlayer)
    {
        players.Add(playerId,new Player(playerId, entityId, playerName));
        playerNames[playerId].UpdateName(playerName);
        callCounters[playerId].InitCounter(players[playerId]);
        if (isLocalPlayer)
            InitLocalPlayer(playerId);
    }

    public void InitMyth(int playerId, int mythTeamIndex, int entityId,
        int unitId, int hp=0, int armor=0, int barrier=0, int attack=0, int range = 1, int attackType = 1, int mobility = 2)
    {
        Debug.Log("Initializing Myth playerId = " + playerId);
        Debug.Log("Initializing Myth players[playerId] = " + players[playerId]);
        players[playerId].Team[mythTeamIndex] = new Myth(playerId, entityId, unitId);
        players[playerId].Team[mythTeamIndex].Stats[Stat.hp] = hp;
        players[playerId].Team[mythTeamIndex].Stats[Stat.armor] = armor;
        players[playerId].Team[mythTeamIndex].Stats[Stat.barrier] = barrier;
        players[playerId].Team[mythTeamIndex].Stats[Stat.attack] = attack;
        players[playerId].Team[mythTeamIndex].Stats[Stat.range] = range;
        players[playerId].Team[mythTeamIndex].Stats[Stat.attackType] = attackType;
        players[playerId].Team[mythTeamIndex].Stats[Stat.mobility] = mobility;
        int index = 5 * Mathf.Abs(playerId - localPlayerId) + mythTeamIndex;
        Debug.Log("Initializing Myth index = " + index);
        mythPortraits[index].LinkedMyth = players[playerId].Team[mythTeamIndex];
        mythPortraits[index].maxHpTag.text = hp.ToString();
        mythPortraits[index].maxArmorTag.text = armor.ToString();
        mythPortraits[index].maxBarrierTag.text = barrier.ToString();
        mythPortraits[index].UpdateMyth();

    }

    public void InitLocalPlayer(int playerId)
    {
        localPlayerId = playerId;
        cameraTransform.position = new Vector3(0, 55, -10+60*playerId);

        cameraTransform.eulerAngles = new Vector3(70, -180 * playerId, 0);


        string[] temp = new string[2];
        temp[0] = playerNames[0].PlayerName;
        temp[1] = playerNames[1].PlayerName;
        playerNames[0].UpdateName(temp[localPlayerId]);
        playerNames[1].UpdateName(temp[1-localPlayerId]);
        try
        {
            callCounters[0].InitCounter(players[localPlayerId]);
        }
        catch (Exception) { }

        try
        {
            callCounters[1].InitCounter(players[1 - localPlayerId]);
        }
        catch (Exception) { }

        //Gauge panel
        gaugePanel.InitGauge(players[localPlayerId]);
        gaugePanel.UpdateGauge();

        TileBehaviour.InitLocalPlayer(playerId);
    }

    public void BeginTurn(int playerId)
    {
        currentPlayer = playerId;
        //Update stats
        foreach(Entity entity in players[playerId].Team)
        {
            if(entity is Unit unit)
            {
                if (entity.Stats[Stat.isCalled] == 1)
                {
                    entity.Stats[Stat.canMove] = 1;
                    entity.Stats[Stat.canAttack] = 1;
                    if(entity.Stats[Stat.isEngaged] == 0)
                    {
                        entity.Stats[Stat.canRecall] = 1;
                    }
                }
            }
        }

        if(playerId == localPlayerId)
        {
            yourTurnObject.SetActive(true);
        }

        playerNames[0].UpdateCurrentPlayer((playerId == localPlayerId));
        playerNames[1].UpdateCurrentPlayer((playerId != localPlayerId));

        turnCounter.UpdateCounter();
        turnCounter.turnCounter++;
        
        timer.resetTimer();
        RefreshInterface();
        
    }

    public void EndGame(int winnerId)
    {
        this.gameStarted = false;
        if(winnerId == players[localPlayerId].Id)
        {
            winScreen.SetActive(true);
        }
        else
        {
            loseScreen.SetActive(true);
        }
        RefreshInterface();
    }
    public void SpellCast(int casterId,int spellId, int x, int y)
    {
        Debug.Log("Spell cast by " + entities[casterId] + " on tile " + x + " " + y);
        Unit caster = (Unit)entities[casterId];
        caster.Model.GetComponentInChildren<Animator>().Play("Base Layer.Attack", 0, 1);

        Instantiate(spellPrefab, new Vector3(-30 + 10 * x, 3, 40 - 10 * y),
            Quaternion.identity);

        history.AddSpell(Spell.ParseSpell(spellId));
        RefreshInterface();
    }

    public void InitPortal(int entityId, int x, int y)
    {
        new Portal(entityId);
        entities[entityId].Stats[Stat.x] = x;
        entities[entityId].Stats[Stat.y] = y;
        TileBehaviour.board[10 * x + y].InitPortal();
        RefreshInterface();
    }

    public void CapturePortal(int entityId, int team)
    {
        int x = entities[entityId].Stats[Stat.x];
        int y = entities[entityId].Stats[Stat.y];
        TileBehaviour.board[10 * x + y].CapturePortal(team);
        RefreshInterface();
    }

    #endregion

    #region Unit management methods

    public void Call(Unit unit, int x, int y) //MESAGE REACTION METHOD
    {
        //Update stats
        unit.Stats[Stat.isCalled] = 1;
        unit.Stats[Stat.canMove] = 1;
        unit.Stats[Stat.canAttack] = 1;
        unit.Stats[Stat.x] = x;
        unit.Stats[Stat.y] = y;
        unit.Stats[Stat.canRecall] = 0;

        Debug.Log("Calling " + unit.Name + " in position " + unit.Stats[Stat.x]+" "+ unit.Stats[Stat.y]);

        unit.Model = Instantiate(Resources.Load("Units/"+unit.Name+"/UnitModel"),
            new Vector3(-30+10*unit.Stats[Stat.x], 2.5f, 38-10*unit.Stats[Stat.y]+4*localPlayerId), 
            cameraTransform.rotation) as GameObject;


        unit.Model.GetComponentInChildren<Animator>().Play("Base Layer.Run", 0, 2);
        if(unit.OwnerId == GameManager.gm.localPlayerId)
        {
            unit.Model.transform.localScale = new Vector3(-1, 1, 1);
        }

        //Update panel
        if(unit is Myth myth && unit.OwnerId == localPlayerId)
        {
            //Init panels
            foreach (MythPanelBehaviour panel in panels)
            {
                if (!panel.gameObject.activeSelf)
                {
                    panel.gameObject.SetActive(true);
                    panel.InitPanel(myth);
                    break;
                }
            }
        }

        RefreshInterface();
    }

    public void UnCall(Unit unit) //MESSAGE REACTION METHOD (Death or recall)
    {
        unit.Stats[Stat.isCalled] = 0;
        unit.Stats[Stat.isEngaged] = 1;

        unit.Model.SetActive(false);

        //Update panels
        if(unit is Myth myth && myth.OwnerId == GameManager.gm.localPlayerId)
        {
            foreach(MythPanelBehaviour panel in panels)
            {
                if(panel.LinkedMyth == myth)
                {
                    panel.MythRecalled();
                }
            }
        }

        RefreshInterface();
    }

    public void MoveUnit(Unit unit, int x, int y)
    {
        //Update stats
        unit.Stats[Stat.x] = x;
        unit.Stats[Stat.y] = y;

        Vector3 targetPos = new Vector3(-30 + 10 * unit.Stats[Stat.x], 3, 38 - 10 * unit.Stats[Stat.y] + 4 * localPlayerId);
        iTween.MoveTo(unit.Model, targetPos, 2);
        unit.Model.GetComponentInChildren<Animator>().Play("Base Layer.Run", 0, 1);
        
        RefreshInterface();

    }

    public void Attack(Unit attacker)
    {
        attacker.Stats[Stat.canAttack] = 0;
        attacker.Model.GetComponentInChildren<Animator>().Play("Base Layer.Attack", 0, 1);
        RefreshInterface();
    }

    public void EntityStatChanged(Entity target, Stat stat, int newValue)
    {
        
        //Bubble 
        if(stat == Stat.hp)
        {
            foreach (TileBehaviour tile in TileBehaviour.board.Values)
            {
                if (UnitOnTile(tile.x, tile.y) == target)
                {
                    tile.UpdateBubble(newValue - target.Stats[stat],0);
                }
            }
        }
        if (stat == Stat.armor)
        {
            foreach (TileBehaviour tile in TileBehaviour.board.Values)
            {
                if (UnitOnTile(tile.x, tile.y) == target)
                {
                    tile.UpdateBubble(newValue - target.Stats[stat], 1);
                }
            }
        }
        if (stat == Stat.barrier)
        {
            foreach (TileBehaviour tile in TileBehaviour.board.Values)
            {
                if (UnitOnTile(tile.x, tile.y) == target)
                {
                    tile.UpdateBubble(newValue - target.Stats[stat], 2);
                }
            }
        }
        //update value
        target.Stats[stat] = newValue;

        RefreshInterface();
        
    }

    public void RefreshInterface()
    {
        //update portraits
        foreach (MythUIBehaviour portrait in mythPortraits)
        {
            portrait.UpdateMyth();
        }

        //TODO optimize
        callCounters[0].UpdateCounter();
        callCounters[1].UpdateCounter();
        foreach (MythPanelBehaviour panel in panels)
        {
            if (panel.LinkedMyth != null)
            {
                panel.UpdatePanel();
            }

        }
        foreach (TileBehaviour tile in TileBehaviour.board.Values)
        {
            tile.UpdateUnitOnTile();
        }

        //Gauge
        gaugePanel.UpdateGauge();
    }
    #endregion

    #region Message processing methods
    public static void OnInitPlayer(object[] parameters)
    {
        if(parameters[0] is int playerId &&
            parameters[1] is int entityId &&
            parameters[2] is string playerName &&
            parameters[3] is bool isLocalPlayer )
        {
            GameManager.gm.InitPlayer(playerId, entityId, playerName, isLocalPlayer);
        }
    }

    public static void OnInitMyth(object[] parameters)
    {
        if (parameters[0] is int playerId &&
            parameters[1] is byte teamIndex &&
            parameters[2] is int entityId &&
            parameters[3] is int unitId &&
            parameters[4] is int hp &&
            parameters[5] is int armor &&
            parameters[6] is int barrier &&
            parameters[7] is int attack &&
            parameters[8] is int range &&
            parameters[9] is int attackType &&
            parameters[10] is int mobility)
        {
            GameManager.gm.InitMyth(playerId,teamIndex,entityId,unitId,hp,armor,barrier,attack, range, attackType, mobility);
        }
    }

    public static void OnEntityStatCHanged(object[] parameters)
    {
        if(parameters[0] is int targetId &&
            parameters[1] is Stat stat &&
            parameters[2] is int newValue)
        {
            GameManager.gm.EntityStatChanged(GameManager.gm.entities[targetId], stat, newValue);
        }
    }

    public static void OnUnitCalled(object[] parameters)
    {
        if (parameters[0] is int targetId &&
            parameters[1] is int x &&
            parameters[2] is int y )
        {
            GameManager.gm.Call((Unit)GameManager.gm.entities[targetId],x,y);
        }
    }

    public static void OnUnitMoved(object[] parameters)
    {
        if (parameters[0] is int targetId &&
            parameters[1] is int x &&
            parameters[2] is int y)
        {
            GameManager.gm.MoveUnit((Unit)GameManager.gm.entities[targetId],x,y);
        }
    }

    public static void OnUnitAttack(object[] parameters)
    {
        if (parameters[0] is int targetId)
        {
            GameManager.gm.Attack((Unit)GameManager.gm.entities[targetId]);
        }
    }

    public static void OnUnitUncalled(object[] parameters)
    {
        if (parameters[0] is int targetId)
        {
            GameManager.gm.UnCall((Unit)GameManager.gm.entities[targetId]);
        }
    }

    public static void OnBeginTurn(object[] parameters)
    {
        if (parameters[0] is int playerId)
        {
            GameManager.gm.BeginTurn(playerId);
        }
    }

    public static void OnEndGame(object[] parameters)
    {
        if (parameters[0] is int winnerId)
        {
            GameManager.gm.EndGame(winnerId);
        }
    }

    public static void OnSpellCast(object[] parameters)
    {
        if (parameters[0] is int casterId &&
            parameters[1] is int spellId &&
            parameters[2] is int x &&
            parameters[3] is int y )
        {
            GameManager.gm.SpellCast(casterId,spellId,x,y);
        }
    }

    public static void OnInitPortal(object[] parameters)
    {
        if (parameters[0] is int entityId &&
            parameters[1] is int x &&
            parameters[2] is int y)
        {
            GameManager.gm.InitPortal(entityId, x, y);
        }
    }

    public static void OnCapturePortal(object[] parameters)
    {
        if (parameters[0] is int entityId &&
            parameters[1] is int team)
        {
            GameManager.gm.CapturePortal(entityId, team);
        }
    }

    #endregion
}
