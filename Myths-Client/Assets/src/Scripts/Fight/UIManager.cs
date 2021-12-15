using Myths_Library;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{

    public static UIManager m;


    //Linked objects
    [SerializeField] private MythUI[] mythPortraits;
    [SerializeField] private PlayerName[] playerNames;
    [SerializeField] private ManaCounter[] manaCounters;
    [SerializeField] private MasteryPanel[] masteryPanels;
    [SerializeField] private SpellBarManager spellBar;
    [SerializeField] private GameObject winScreen, loseScreen;
    [Header("Mastery Colors")]
    public Color colorLight;
    public Color colorDark;
    public Color colorFire;
    public Color colorWater;
    public Color colorEarth;
    public Color colorAir;
    //Events
    public Dictionary<Mastery, Color> masteryColor;
    [HideInInspector] public UnityEvent<Stat,int> onEntityStatChanged = new UnityEvent<Stat, int>();

    void Awake()
    {
        m = this;
    }

    public void Start()
    {
        masteryColor = new Dictionary<Mastery, Color>
        {
            {Mastery.light,colorLight },
            {Mastery.dark,colorDark },
            {Mastery.fire,colorFire },
            {Mastery.water,colorWater },
            {Mastery.air,colorAir },
            {Mastery.earth,colorEarth }
        };
    }

    public void RefreshInterface()
    {
        //update portraits
        foreach (MythUI portrait in mythPortraits)
        {
            portrait.UpdateMyth();
        }

        spellBar.UpdateSpellBar();
        manaCounters[0].UpdateCounter();
        manaCounters[1].UpdateCounter();
        masteryPanels[0].UpdateMasteries();
        masteryPanels[1].UpdateMasteries();

    }
    #region Init
    public void InitPlayer(int playerId, string playerName)
    {
        playerNames[playerId].UpdateName(playerName);
        manaCounters[playerId].InitCounter(GameManager.gm.players[playerId]);
        masteryPanels[playerId].Init(GameManager.gm.players[playerId]);
    }

    public void InitMyth(int index, Myth myth)
    {
        mythPortraits[index].Init(myth);
    }

    public void InitLocalPlayer()
    {
        string[] temp = new string[2];
        temp[0] = playerNames[0].UserName;
        temp[1] = playerNames[1].UserName;
        playerNames[0].UpdateName(temp[GameManager.gm.localPlayerId]);
        playerNames[1].UpdateName(temp[1 - GameManager.gm.localPlayerId]);

        manaCounters[0].InitCounter(GameManager.gm.players[GameManager.gm.localPlayerId]);
        manaCounters[1].InitCounter(GameManager.gm.players[1 - GameManager.gm.localPlayerId]);

        masteryPanels[0].Init(GameManager.gm.players[GameManager.gm.localPlayerId]);
        masteryPanels[1].Init(GameManager.gm.players[1 - GameManager.gm.localPlayerId]);
    }

    #endregion

    #region End Game

    public void Win()
    {
        winScreen.SetActive(true);
    }

    public void Lose()
    {
        loseScreen.SetActive(true);
    }
    #endregion

    #region Events
    public void BeginTurnUpdate(int playerId)
    {
        playerNames[0].UpdateCurrentPlayer((playerId == GameManager.gm.localPlayerId));
        playerNames[1].UpdateCurrentPlayer((playerId != GameManager.gm.localPlayerId));
    }

    public void OnEntityStatChanged(Stat stat, int newValue)
    {
        onEntityStatChanged.Invoke(stat, newValue);
    }


    public void Call(Myth myth)
    {
        if(myth.OwnerId == GameManager.gm.localPlayerId)
            spellBar.AddMyth(myth);
    }
    #endregion
}
