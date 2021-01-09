﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * MenuManager
 * Script tha manages the LoginScene diplay and interactions
 */
public class MenuManager : MonoBehaviour
{

    #region Variables
    public static MenuManager menuManager;

    public GameObject connectingText;
    public GameObject enterWindow;
    public GameObject connectedWindow;
    public GameObject loggedInWindow;
    public GameObject inQueueWindow;
    public GameObject teambuilderWindow;
    public SelectedTeamBehaviour selectedTeam;
    public TeambuilderBehaviour teambuilder;
    //For interface update on main threat purpose
    private static Queue<Action> menuUpdates = new Queue<Action>();
    #endregion

    #region Unity Methods

    private void Awake()
    {
        initPrefs:

        int firstLoad = PlayerPrefs.GetInt("First load");
        if(firstLoad == 0)
        {
            PlayerPrefs.SetInt("Myth1", 4);
            PlayerPrefs.SetInt("Myth2", 0);
            PlayerPrefs.SetInt("Myth3", 1);
            PlayerPrefs.SetInt("Myth4", 3);
            PlayerPrefs.SetInt("Myth5", 9);
            PlayerPrefs.SetInt("First load", 1);
            goto initPrefs;
        }
        else
        {
            int myth1 = PlayerPrefs.GetInt("Myth1");
            int myth2 = PlayerPrefs.GetInt("Myth2");
            int myth3 = PlayerPrefs.GetInt("Myth3");
            int myth4 = PlayerPrefs.GetInt("Myth4");
            int myth5 = PlayerPrefs.GetInt("Myth5");

            selectedTeam.AddMyth(Myth.ParseMyth(myth1));
            selectedTeam.AddMyth(Myth.ParseMyth(myth2));
            selectedTeam.AddMyth(Myth.ParseMyth(myth3));
            selectedTeam.AddMyth(Myth.ParseMyth(myth4));
            selectedTeam.AddMyth(Myth.ParseMyth(myth5));

        }




    }

    private void Start()
    {
        MenuManager.menuManager = this;
        enterWindow.SetActive(true);
        
    }

    void Update()
    {
        while(menuUpdates.Count>0)
        {
            menuUpdates.Dequeue().Invoke();
        }
        
    }

    #endregion

    #region Buttons Methods

    public void OnLocalButtonPressed()
    {
        connectingText.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        Thread connectionThread = new Thread(() => Server.Connect("127.0.0.1"));
        connectionThread.IsBackground = true;
        connectionThread.Start();

        //Update menu window
        enterWindow.SetActive(false);
        connectingText.GetComponent<UnityEngine.UI.Text>().text = "Connecting...";
    }

    public void OnOnlineButtonPressed()
    {
        connectingText.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);

        //Thread connectionThread = new Thread(() => Server.Connect("90.92.24.188"));
        Thread connectionThread = new Thread(() => Server.Connect("86.247.113.227"));
        //Thread connectionThread = new Thread(() => Server.Connect("51.38.239.230"));

        connectionThread.IsBackground = true;
        connectionThread.Start();

        //Update menu window
        enterWindow.SetActive(false);
        connectingText.GetComponent<UnityEngine.UI.Text>().text = "Connecting...";
    }

    public void OnTeambuilderButtonPressed()
    {
        enterWindow.SetActive(false);
        
        teambuilderWindow.SetActive(true);
        teambuilder.InitTeambuilder();
    }

    public void OnLeaveBuilderButtonPressed()
    {
        teambuilder.ExitTeambuilder();
        teambuilderWindow.SetActive(false);
        PlayerPrefs.SetInt("Myth1", selectedTeam.myths[0].LinkedMyth.Id);
        PlayerPrefs.SetInt("Myth2", selectedTeam.myths[1].LinkedMyth.Id);
        PlayerPrefs.SetInt("Myth3", selectedTeam.myths[2].LinkedMyth.Id);
        PlayerPrefs.SetInt("Myth4", selectedTeam.myths[3].LinkedMyth.Id);
        PlayerPrefs.SetInt("Myth5", selectedTeam.myths[4].LinkedMyth.Id);
        enterWindow.SetActive(true);
    }
    public void OnLoginButtonPressed()
    {

        string username = connectedWindow.GetComponentInChildren<UnityEngine.UI.Text>().text;

        if (username.Equals(""))
        {
            username = "Player Name";
        }

        Debug.Log("Message to send : " + username+"END");
        Server.SendMessageToServer(new LoginMessage(username));

        //Update menu window
        connectedWindow.SetActive(false);
        connectingText.GetComponent<UnityEngine.UI.Text>().text = "Logging in ...";
    }

    public void OnLogoutButtonPressed()
    {
        Server.SendMessageToServer(new LogoutMessage());

        //Update menu window
        loggedInWindow.SetActive(false);
        connectingText.GetComponent<UnityEngine.UI.Text>().text = "Logging out ...";
    }

    public void OnEnterQueueButtonPressed()
    {

        Server.SendMessageToServer(new JoinQueueMessage(
            selectedTeam.myths[0].LinkedMyth.Id,
            selectedTeam.myths[1].LinkedMyth.Id,
            selectedTeam.myths[2].LinkedMyth.Id,
            selectedTeam.myths[3].LinkedMyth.Id,
            selectedTeam.myths[4].LinkedMyth.Id
            ));

        //Update menu window
        loggedInWindow.SetActive(false);
        connectingText.GetComponent<UnityEngine.UI.Text>().text = "Entering queue ...";
    }

    public void OnLeaveQueueButtonPressed()
    {
        Server.SendMessageToServer(new LeaveQueueMessage());

        //Update menu window
        inQueueWindow.SetActive(false);
        connectingText.GetComponent<UnityEngine.UI.Text>().text = "Leaving queue ...";
    }

    #endregion

    #region Server Message Reaction Methods
    public static void UpdateDisplay(Action action)
    {
        menuUpdates.Enqueue(action);
    }

    #endregion

    #region Menu Display Update Methods
    public void OnConnectionFailed()
    {
        enterWindow.SetActive(true);
        connectingText.GetComponent<UnityEngine.UI.Text>().text = "Connection failed";
    }
    public void OnConnected()
    {
        connectedWindow.SetActive(true);
        connectingText.GetComponent<UnityEngine.UI.Text>().text = "Connected";
    }

    public void OnLoggedIn()
    {
        loggedInWindow.SetActive(true);
        connectingText.GetComponent<UnityEngine.UI.Text>().text = "Welcome : "+Server.username;
    }

    public void OnLoggedOut()
    {
        connectedWindow.SetActive(true);
        connectingText.GetComponent<UnityEngine.UI.Text>().text = "Connected";
    }

    public void OnQueueJoined()
    {
        inQueueWindow.SetActive(true);
        connectingText.GetComponent<UnityEngine.UI.Text>().text = "Looking for a match...";
    }

    public void OnQueueLeft()
    {
        loggedInWindow.SetActive(true);
        connectingText.GetComponent<UnityEngine.UI.Text>().text = "Welcome : " + Server.username;
    }

    public void OnMatchFound()
    {
        SceneManager.LoadScene("FightScene");
    }
    #endregion

}

