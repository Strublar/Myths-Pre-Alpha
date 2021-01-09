using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Myths_Server
{
    /**
     * User
     * Manages the user's socket, listens and process incoming messages
     */
    public class User
    {
        #region Static Variables

        public static int PoolCount = 0;

        #endregion

        #region Attributes

        private int id;
        private string username;
        private UserMode currentUserMode;
        private TcpClient client = null;
        private NetworkStream clientStream = null;
        private Thread listeningClient = null;
        private Game game;
        private int[] team;

        private Dictionary<byte, Action<byte[]>> messageProcessor;
        #endregion

        #region Getters & Setters

        public int Id { get => id; set => id = value; }
        public TcpClient Client { get => client; set => client = value; }
        public NetworkStream ClientStream { get => clientStream; set => clientStream = value; }
        public string Username { get => username; set => username = value; }
        public UserMode CurrentUserMode { get => currentUserMode; set => currentUserMode = value; }

        public Game Game { get => game; set => game = value; }
        public int[] Team { get => team; set => team = value; }

        #endregion

        #region Constructor

        public User()
        {
            id = PoolCount;
            User.PoolCount++;
            currentUserMode = UserMode.Connected;
            InitMessageProcessor();
        }
        public User(TcpClient client)
        {
            

            id = PoolCount;
            User.PoolCount++;
            Console.WriteLine("New client created with id " + id);
            InitMessageProcessor();

            this.client = client;
            this.clientStream = this.client.GetStream();

            listeningClient = new Thread(new ThreadStart(ListeningSocket));
            listeningClient.IsBackground = true;    
            listeningClient.Start();
        }

        public void InitMessageProcessor()
        {
            messageProcessor = new Dictionary<byte, Action<byte[]>>();
            messageProcessor.Add((byte)ClientMessageType.Login, Login);
            messageProcessor.Add((byte)ClientMessageType.Logout, Logout);
            messageProcessor.Add((byte)ClientMessageType.JoinQueue, JoinQueue);
            messageProcessor.Add((byte)ClientMessageType.LeaveQueue, LeaveQueue);
            messageProcessor.Add((byte)ClientMessageType.Call, OnCall);
            messageProcessor.Add((byte)ClientMessageType.Recall, OnRecall);
            messageProcessor.Add((byte)ClientMessageType.Attack, OnAttack);
            messageProcessor.Add((byte)ClientMessageType.CastSpell, OnCastSpell);
            messageProcessor.Add((byte)ClientMessageType.Move, OnMove);
            messageProcessor.Add((byte)ClientMessageType.EndTurn, OnEndTurn);
        }
        #endregion


        #region Listening Methods
        /**
         * ListeningSocket
         * Listen to incoming messages from the user's socket
         */
        public void ListeningSocket()
        {
            Console.WriteLine("New Client " + id + " Listening Thread opened");

            while (true)
            {
                StreamReader sr = new StreamReader(clientStream);
                StreamWriter sw = new StreamWriter(clientStream);
                try
                {
                    byte[] buffer = new byte[1024];
                    clientStream.Read(buffer, 0, buffer.Length);
                    int recv = 0;
                    foreach (byte b in buffer)
                    {
                        if (b != 0)
                        {
                            recv++;
                        }
                    }
                    string request = Encoding.UTF8.GetString(buffer, 0, recv);
                    Console.WriteLine("Request received from user "+id+" :" + request);

                    if(request.Equals(""))
                    {
                        Console.WriteLine("Empty message recieved, Terminating Session");
                        clientStream.Close();
                        break;
                    }
                    ProcessMessage(buffer);

                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong. \n" + e);
                    //sw.WriteLine(e.ToString());
                    clientStream.Close();
                    break;

                }
            }
        }
        #endregion

        #region Message Process Methods

        /**
         * ProcessMessage
         * Read the message type of the message in parameter
         * @param message : Contains the message of the user
         */
        public void ProcessMessage(byte[] message)
        {
            messageProcessor[message[0]].Invoke(message);
        }

        
        
        #endregion

        #region User Methods

        #region Auth methods

        /**
         * Login
         * Sets up the user to a logged in state with its username
         * @param newUserName : User name
         */
        public void Login(byte[] message)
        {
            string newUserName = Encoding.UTF8.GetString(message, 1, message.Length - 2);
            username = newUserName.Trim('\n').Trim().Trim('\0').Trim('\r');
            Console.WriteLine("User " + id + " has logged in with name " + username+" Test length : "+username.Length);
            this.currentUserMode = UserMode.LoggedIn;
            MythsServer.SendMessage(this, new LoggedInMessage(newUserName));
        }

        /**
         * Logout
         * Remove the user from logged in state
         */
        public void Logout(byte[] message)
        {
            username = null;
            Console.WriteLine("User " + id + " has logged out");
            this.currentUserMode = UserMode.Connected;
            MythsServer.SendMessage(this, new LoggedOutMessage());
        }

        /**
         * EnterQueue
         * Put the user in the matchmaking queue
         */
        public void JoinQueue(byte[] message)
        {
            Console.WriteLine("User " + id + " has entered queue");
            this.currentUserMode = UserMode.InQueue;

            this.Team = new int[]
            {
                Utils.ParseInt(message,1),
                Utils.ParseInt(message,5),
                Utils.ParseInt(message,9),
                Utils.ParseInt(message,13),
                Utils.ParseInt(message,17),
            };
            MythsServer.SendMessage(this, new QueueJoinedMessage());
            MythsServer.UserJoinQueue(this);
        }
        /**
         * LeaveQueue
         * Remove the user from the matchmaking
         */
        public void LeaveQueue(byte[] message)
        {
            Console.WriteLine("User " + id + " has left queue");
            this.currentUserMode = UserMode.LoggedIn;
            MythsServer.UserLeftQueue(this);

            MythsServer.SendMessage(this, new QueueLeftMessage());
        }

        #endregion

        #region Fight Methods

        public void OnAttack(byte[] message)
        {
            int attackerId = Utils.ParseInt(message, 1);
            int targetId = Utils.ParseInt(message, 5);
            object[] parameters = { attackerId, targetId };
            if(game.CurrentPlayer == this)
            {
                this.game.WorkerQueue.Enqueue(this.game.OnAttack);
                this.game.WorkerQueueParameters.Enqueue(parameters);
            }
        }

        public void OnCall(byte[] message)
        {
            int targetId = Utils.ParseInt(message, 1);
            int playerId = Utils.ParseInt(message, 5);
            int x = Utils.ParseInt(message, 9);
            int y = Utils.ParseInt(message, 13);
            object[] parameters = { targetId,playerId,x,y };
            if (game.CurrentPlayer == this)
            {
                this.game.WorkerQueue.Enqueue(this.game.OnCall);
                this.game.WorkerQueueParameters.Enqueue(parameters);
            }
        }

        public void OnCastSpell(byte[] message)
        {
            int casterId = Utils.ParseInt(message, 1);
            int spellId = Utils.ParseInt(message, 5);
            int x = Utils.ParseInt(message, 9);
            int y = Utils.ParseInt(message, 13);
            object[] parameters = { casterId, spellId, x, y };
            if (game.CurrentPlayer == this)
            {
                this.game.WorkerQueue.Enqueue(this.game.OnCastSpell);
                this.game.WorkerQueueParameters.Enqueue(parameters);
            }
        }

        public void OnEndTurn(byte[] message)
        {
            object[] parameters = { this};
            if (game.CurrentPlayer == this)
            {
                this.game.WorkerQueue.Enqueue(this.game.OnEndTurn);
                this.game.WorkerQueueParameters.Enqueue(parameters);
            }
        }

        public void OnMove(byte[] message)
        {
            int targetId = Utils.ParseInt(message, 1);
            int x = Utils.ParseInt(message, 5);
            int y = Utils.ParseInt(message, 9);
            object[] parameters = { targetId, x, y };
            if (game.CurrentPlayer == this)
            {
                this.game.WorkerQueue.Enqueue(this.game.OnMove);
                this.game.WorkerQueueParameters.Enqueue(parameters);
            }
        }

        public void OnRecall(byte[] message)
        {
            int targetId = Utils.ParseInt(message, 1);
            int playerId = Utils.ParseInt(message, 5);
            object[] parameters = { targetId, playerId };
            if (game.CurrentPlayer == this)
            {
                this.game.WorkerQueue.Enqueue(this.game.OnRecall);
                this.game.WorkerQueueParameters.Enqueue(parameters);
            }
        }


        #endregion

        #endregion

        #region Other Methods

        #endregion
    }
}
