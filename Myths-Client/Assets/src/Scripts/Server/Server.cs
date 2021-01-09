using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Auth
 * Initalize and manage the interactions with the server
 * Send, recieve and process the messages from the server
 */
public class Server : MonoBehaviour
{

    #region Variables
    //Network relative variables
    public static Thread listeningThread;
    public static NetworkStream clientStream;
    private static Dictionary<byte, Action<byte[]>> messageProcessor;
    public static Queue<byte[]> workerQueue;

    //User relative variables
    public static UserMode currentUserMode = UserMode.Launching;
    public static int userId;
    public static string username = "player name";
    public static string opponentUsername;
    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Server.messageProcessor = new Dictionary<byte, Action<byte[]>>();
        Server.messageProcessor.Add((byte)ServerMessageType.LoggedIn, OnLoggedIn);
        Server.messageProcessor.Add((byte)ServerMessageType.LoggedOut, OnLoggedOut);
        Server.messageProcessor.Add((byte)ServerMessageType.QueueJoined, OnQueueJoined);
        Server.messageProcessor.Add((byte)ServerMessageType.QueueLeft, OnQueueLeft);
        Server.messageProcessor.Add((byte)ServerMessageType.MatchFound, OnMatchFound);


        //Fight messages
        //Start game messages
        Server.messageProcessor.Add((byte)ServerMessageType.InitPlayer, OnInitPlayer);
        Server.messageProcessor.Add((byte)ServerMessageType.InitMyth, OnInitMyth);
        Server.messageProcessor.Add((byte)ServerMessageType.StartGame, OnStartGame);

        //other fight messages
        Server.messageProcessor.Add((byte)ServerMessageType.EntityStatChanged, OnEntityStatChanged);
        Server.messageProcessor.Add((byte)ServerMessageType.UnitCalled, OnUnitCalled);
        Server.messageProcessor.Add((byte)ServerMessageType.UnitMoved, OnUnitMoved);
        Server.messageProcessor.Add((byte)ServerMessageType.UnitAttack, OnUnitAttack);
        Server.messageProcessor.Add((byte)ServerMessageType.UnitUncalled, OnUnitUncalled);
        Server.messageProcessor.Add((byte)ServerMessageType.BeginTurn, OnBeginTurn);
        Server.messageProcessor.Add((byte)ServerMessageType.EndGame, OnEndGame);
        Server.messageProcessor.Add((byte)ServerMessageType.SpellCast, OnSpellCast);
        Server.messageProcessor.Add((byte)ServerMessageType.InitPortal, OnInitPortal);
        Server.messageProcessor.Add((byte)ServerMessageType.CapturePortal, OnCapturePortal);

        Server.workerQueue = new Queue<byte[]>();
        //test
        /*byte[] player1 = {(byte)ServerMessageType.InitPlayer,0,0,0,0,0,0,0,0,1 };
        byte[] playerName1 = Encoding.UTF8.GetBytes("player1");
        int originalLength = player1.Length;
        Array.Resize<byte>(ref player1, originalLength + playerName1.Length);
        Array.Copy(playerName1, 0, player1, originalLength, playerName1.Length);
        byte[] player2 = { (byte)ServerMessageType.InitPlayer, 0, 0, 0, 1, 0, 0, 0, 1, 0 };
        byte[] playerName2 = Encoding.UTF8.GetBytes("player2");
        int originalLength2 = player2.Length;
        Array.Resize<byte>(ref player2, originalLength2 + playerName2.Length);
        Array.Copy(playerName2, 0, player2, originalLength2, playerName2.Length);
        OnInitPlayer(player1);
        OnInitPlayer(player2);*/
    }
    


    private void Update()
    {
        try
        {
            while (Server.workerQueue.Count > 0)
            {
                /*Debug.Log("Messages in queue : " + workerQueue.Count);
                Debug.Log("Processing a new Message");*/
                Server.ProcessMessage(Server.workerQueue.Dequeue());
            }
        }
        catch (Exception) { }
        
        
    }
    #endregion

    #region Connection Methods

    /**
     * Connect
     * Connect to the server with ip in parameters
     * @param ip : Ip address of the server
     */
    public static void Connect(string ip)
    {
        int connectionTry = 0;
    connection:
        try
        {
            //TcpClient client = new TcpClient("90.92.24.188", 1301);
            //TcpClient client = new TcpClient("127.0.0.1", 1301);

            Server.currentUserMode = UserMode.Connecting;

            TcpClient client = new TcpClient(ip, 1301);
            Server.clientStream = client.GetStream();
            Server.listeningThread = new Thread(new ThreadStart(ListeningSocket));
            Server.listeningThread.IsBackground = true;
            Server.listeningThread.Start();

            Server.currentUserMode = UserMode.Connected;
            MenuManager.UpdateDisplay(MenuManager.menuManager.OnConnected);
        }
        catch (Exception e)
        {
            Debug.Log("failed to connect..." + e);
            connectionTry++;
            if(connectionTry <3)
            {
                goto connection;
            }
            else
            {
                Debug.Log("Connection failed");
                Server.currentUserMode = UserMode.Launching;
                MenuManager.UpdateDisplay(MenuManager.menuManager.OnConnectionFailed);
            }
        }
    }

    #endregion

    #region Listening Thread Methods

    /**
     * ListeningSocket
     * Wait for a server message then process it
     * (only used in a listening thread)
     */
    public static void ListeningSocket()
    {

        while (true)
        {
            try
            {

                
                byte[] buffer = new byte[256];
                int messageLength;
                while((messageLength = Server.clientStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    if(messageLength==256)
                    {
                        //string request = "";
                        int counterNull = 0;
                        foreach (byte b in buffer)
                        {
                            if (b != 0)
                                counterNull++;
                            //request += b.ToString() + " ";
                        }
                        //Debug.Log("Request received of length : " + request);
                        if (counterNull == 0)
                        {
                            Debug.Log("Empty message recieved, Terminating Session");
                            Server.clientStream.Close();
                            break;
                        }

                        workerQueue.Enqueue((byte[])buffer.Clone());
                    }
                    
                    buffer = new byte[256];
                }
                
            }
            catch (Exception e)
            {
                Debug.Log("Something went wrong." + e);
                //sw.WriteLine(e.ToString());
                break;
            }
        }
    }
    #endregion

    #region Communication Methods

    /**
    * SendMessage (authMessage overload)
    * Send a message to the user in parameter
    * @param messageType : Type of the message (Auth/Combat...)
    * @param authMessageType : Type of auth message (LogIn/Logout...)
    * @param value : Data field of the message
    */
    public static void SendMessageToServer(Message message)
    {



        //Sending message
        try
        {
            clientStream.Write(message.GetBytes(), 0, 256);
            Debug.Log(message + " sent");
        }
        catch (Exception e)
        {
            Debug.Log("Error : " + e);
        }
    }
    //TODO autres types de messages (combat)

    #endregion

    #region Message Process Methods

    /**
    * ProcessMessage
    * Read the message type of the message in parameter
    * @param message : Contains the message of the server
    */
    public static void ProcessMessage(byte[] message)
    {
        try
        {
            messageProcessor[message[0]].Invoke(message);
        }
        catch(KeyNotFoundException)
        {
            Debug.LogError("Key not found in message : " + message[0]);
        }
    }

    #endregion

    #region User Methods

    private static void OnLoggedIn(byte[] message)
    {
        Debug.Log("Logged in message received");
        string loginName = Encoding.UTF8.GetString(message, 1, message.Length - 1);
        Server.username = loginName;
        Server.currentUserMode = UserMode.LoggedIn;
        MenuManager.UpdateDisplay(MenuManager.menuManager.OnLoggedIn);
    }

    private static void OnLoggedOut(byte[] message)
    {
        Debug.Log("Logged out Message received");
        Server.currentUserMode = UserMode.Connected;
        MenuManager.UpdateDisplay(MenuManager.menuManager.OnLoggedOut);
    }

    private static void OnQueueJoined(byte[] message)
    {
        Debug.Log("Queue Joined Message received");
        Server.currentUserMode = UserMode.InQueue;
        MenuManager.UpdateDisplay(MenuManager.menuManager.OnQueueJoined);
    }

    private static void OnQueueLeft(byte[] message)
    {
        Debug.Log("Queue Left Message received");
        Server.currentUserMode = UserMode.LoggedIn;
        MenuManager.UpdateDisplay(MenuManager.menuManager.OnQueueLeft);
    }

    private static void OnMatchFound(byte[] message)
    {
        Debug.Log("Match Found Message received");

        Server.currentUserMode = UserMode.MatchFound;
        MenuManager.UpdateDisplay(MenuManager.menuManager.OnMatchFound);
        
    }

    #endregion

    #region Fight Messages

    #region Start Game
    private static void OnInitPlayer(byte[] message)
    {
        Debug.Log("Init Player message recieved");
        

        int playerId = Utils.ParseInt(message, 1);
        int entityId = Utils.ParseInt(message, 5);
        bool isLocalPlayer = (message[9] == 0) ? false : true;
        string playerName = Encoding.UTF8.GetString(message, 10, 32);
        object[] parameters = { playerId, entityId, playerName, isLocalPlayer };
        GameManager.fightUpdates.Enqueue(GameManager.OnInitPlayer);
        GameManager.fightUpdatesParam.Enqueue(parameters);
    }

    private static void OnInitMyth(byte[] message)
    {
        Debug.Log("Init Myth message recieved");
        int playerId = Utils.ParseInt(message, 1);
        byte teamIndex = message[5];
        int entityId = Utils.ParseInt(message, 6);
        int unitId = Utils.ParseInt(message, 10);
        int hp = Utils.ParseInt(message, 14);
        int armor = Utils.ParseInt(message, 18);
        int barrier = Utils.ParseInt(message, 22);
        int attack = Utils.ParseInt(message, 26);
        int range = Utils.ParseInt(message, 30);
        int attackType = Utils.ParseInt(message, 34);
        int mobility = Utils.ParseInt(message, 38);
        object[] parameters = { playerId, teamIndex, entityId, unitId, hp, armor, barrier, attack, range, attackType, mobility };
        GameManager.fightUpdates.Enqueue(GameManager.OnInitMyth);
        GameManager.fightUpdatesParam.Enqueue(parameters);

    }

    private static void OnStartGame(byte[] message)
    {
        //Nothing from now
    }

    private static void OnEntityStatChanged(byte[] message)
    {
        
        int targetId = Utils.ParseInt(message, 1);
        Stat stat = (Stat)message[5];
        int newValue = Utils.ParseInt(message, 6);
        object[] parameters = { targetId, stat, newValue };
        Debug.Log("Entity stat changed Message received, Stat is "+stat+" newvalue is " +newValue);
        GameManager.fightUpdates.Enqueue(GameManager.OnEntityStatCHanged);
        GameManager.fightUpdatesParam.Enqueue(parameters);
    }

    private static void OnUnitCalled(byte[] message)
    {
        
        int targetId = Utils.ParseInt(message, 1);
        int x = Utils.ParseInt(message, 5);
        int y = Utils.ParseInt(message, 9);
        object[] parameters = { targetId,x,y};
        Debug.Log("Unit Called Message received, unit is " +GameManager.gm.entities[targetId].Name+
            " on position "+x+" "+y);
        GameManager.fightUpdates.Enqueue(GameManager.OnUnitCalled);
        GameManager.fightUpdatesParam.Enqueue(parameters);
    }

    private static void OnUnitMoved(byte[] message)
    {
        
        int targetId = Utils.ParseInt(message, 1);
        int x = Utils.ParseInt(message, 5);
        int y = Utils.ParseInt(message, 9);
        object[] parameters = { targetId ,x,y};
        Debug.Log("Unit moved Message received, unit is "+GameManager.gm.entities[targetId].Name+" on "+x+" "+y);
        GameManager.fightUpdates.Enqueue(GameManager.OnUnitMoved);
        GameManager.fightUpdatesParam.Enqueue(parameters);
    }

    private static void OnUnitAttack(byte[] message)
    {
        Debug.Log("Unit attack Message received");
        int targetId = Utils.ParseInt(message, 1);

        object[] parameters = { targetId };
        GameManager.fightUpdates.Enqueue(GameManager.OnUnitAttack);
        GameManager.fightUpdatesParam.Enqueue(parameters);
    }

    private static void OnUnitUncalled(byte[] message)
    {
        Debug.Log("Unit uncall Message received");
        int targetId = Utils.ParseInt(message, 1);

        object[] parameters = { targetId };
        GameManager.fightUpdates.Enqueue(GameManager.OnUnitUncalled);
        GameManager.fightUpdatesParam.Enqueue(parameters);
    }

    private static void OnBeginTurn(byte[] message)
    {
        Debug.Log("Begin turn Message received");
        int playerId = Utils.ParseInt(message, 1);

        object[] parameters = { playerId };
        GameManager.fightUpdates.Enqueue(GameManager.OnBeginTurn);
        GameManager.fightUpdatesParam.Enqueue(parameters);
    }

    private static void OnEndGame(byte[] message)
    {
        Debug.Log("End game Message received");
        int winnerId = Utils.ParseInt(message, 1);

        object[] parameters = { winnerId };
        GameManager.fightUpdates.Enqueue(GameManager.OnEndGame);
        GameManager.fightUpdatesParam.Enqueue(parameters);
    }

    private static void OnSpellCast(byte[] message)
    {
        Debug.Log("Spell cast Message received");
        int casterId = Utils.ParseInt(message, 1);
        int spellId = Utils.ParseInt(message, 5);
        int x = Utils.ParseInt(message, 9);
        int y = Utils.ParseInt(message, 13);
        object[] parameters = { casterId, spellId,x,y };
        GameManager.fightUpdates.Enqueue(GameManager.OnSpellCast);
        GameManager.fightUpdatesParam.Enqueue(parameters);
    }

    private static void OnInitPortal(byte[] message)
    {
        Debug.Log("Init Portal Message received");
        int entityId = Utils.ParseInt(message, 1);
        int x = Utils.ParseInt(message, 5);
        int y = Utils.ParseInt(message, 9);
        object[] parameters = { entityId,  x, y };
        GameManager.fightUpdates.Enqueue(GameManager.OnInitPortal);
        GameManager.fightUpdatesParam.Enqueue(parameters);
    }

    private static void OnCapturePortal(byte[] message)
    {
        Debug.Log("Capture portal Message received");
        int entityId = Utils.ParseInt(message, 1);
        int team = Utils.ParseInt(message, 5);

        object[] parameters = { entityId, team};
        GameManager.fightUpdates.Enqueue(GameManager.OnCapturePortal);
        GameManager.fightUpdatesParam.Enqueue(parameters);
    }
    #endregion

    #endregion
}

