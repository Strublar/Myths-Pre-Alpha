using Myths_Library;
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
    private static MessageProcessor messageProcessor;
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

        messageProcessor = new MessageProcessor();
        messageProcessor.processor.Add(typeof(LoggedInMessage), OnLoggedIn);
        messageProcessor.processor.Add(typeof(LoggedOutMessage), OnLoggedOut);
        messageProcessor.processor.Add(typeof(QueueJoinedMessage), OnQueueJoined);
        messageProcessor.processor.Add(typeof(QueueLeftMessage), OnQueueLeft);
        messageProcessor.processor.Add(typeof(MatchFoundMessage), OnMatchFound);
        messageProcessor.processor.Add(typeof(InitPlayerMessage), OnInitPlayer);
        messageProcessor.processor.Add(typeof(InitMythMessage), OnInitMyth);


        Server.workerQueue = new Queue<byte[]>();


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
            Debug.Log("Connecting to : " + ip);
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
            //messageProcessor[message[0]].Invoke(message);
            Message messageObject = messageProcessor.GenerateServerMessage(message);
            messageObject.ParseMessage(message);
            messageProcessor.processor[messageObject.GetType()].Invoke(messageObject);
            
        }
        catch(KeyNotFoundException)
        {
            Debug.LogError("Key not found in message : " + message[0]);
        }
    }

    #endregion

    #region User Methods

    private static void OnLoggedIn(Message message)
    {
        Debug.Log("Logged in message received");
        Server.username = (message as LoggedInMessage).username;
        Server.currentUserMode = UserMode.LoggedIn;
        MenuManager.UpdateDisplay(MenuManager.menuManager.OnLoggedIn);
    }

    private static void OnLoggedOut(Message message)
    {
        Debug.Log("Logged out Message received");
        Server.currentUserMode = UserMode.Connected;
        MenuManager.UpdateDisplay(MenuManager.menuManager.OnLoggedOut);
    }

    private static void OnQueueJoined(Message message)
    {
        Debug.Log("Queue Joined Message received");
        Server.currentUserMode = UserMode.InQueue;
        MenuManager.UpdateDisplay(MenuManager.menuManager.OnQueueJoined);
    }

    private static void OnQueueLeft(Message message)
    {
        Debug.Log("Queue Left Message received");
        Server.currentUserMode = UserMode.LoggedIn;
        MenuManager.UpdateDisplay(MenuManager.menuManager.OnQueueLeft);
    }

    private static void OnMatchFound(Message message)
    {
        Debug.Log("Match Found Message received");

        Server.currentUserMode = UserMode.MatchFound;
        MenuManager.UpdateDisplay(MenuManager.menuManager.OnMatchFound);
        
    }

    #endregion

    #region Fight Messages

    #region Start Game
    private static void OnInitPlayer(Message message)
    {
        Debug.Log("Init Player message recieved");
        
        if((message as InitPlayerMessage).isLocalPlayer)
        {
            Debug.Log("I am : " + (message as InitPlayerMessage).playerName);
        }
        else
        {
            Debug.Log("opponent is : " + (message as InitPlayerMessage).playerName);
        }

        /*int playerId = Utils.ParseInt(message, 1);
        int entityId = Utils.ParseInt(message, 5);
        bool isLocalPlayer = (message[9] == 0) ? false : true;
        string playerName = Encoding.UTF8.GetString(message, 10, 32);
        object[] parameters = { playerId, entityId, playerName, isLocalPlayer };
        GameManager.fightUpdates.Enqueue(GameManager.OnInitPlayer);
        GameManager.fightUpdatesParam.Enqueue(parameters);*/
    }

    private static void OnInitMyth(Message message)
    {
        Debug.Log("Init Myth message recieved");


        Debug.Log("Myth id is " + (message as InitMythMessage).set.id);
        /*int playerId = Utils.ParseInt(message, 1);
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
        GameManager.fightUpdatesParam.Enqueue(parameters);*/

    }

    #endregion
    /*
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
*/
    #endregion
}

public enum UserMode
{
    Launching,
    Teambuilding,
    Connecting,
    Connected,
    LoggingIn,
    LoggedIn,
    LoggingOut,
    EnteringQueue,
    InQueue,
    LeavingQueue,
    MatchFound

}