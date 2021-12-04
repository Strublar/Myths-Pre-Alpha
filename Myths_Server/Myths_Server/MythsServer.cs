using Myths_Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace Myths_Server
{
    /**
     * MythServer
     * manage incoming connection and sends messages
     */
    public class MythsServer
    {

        private TcpListener listener = null;
        private Thread clientConnection;
        private static Queue mainQueue;
        private static List<Game> games;

        private static Dictionary<int,User> connectedUsers;
        


        #region Getters & Setters
        public static Queue MainQueue { get => mainQueue; set => mainQueue = value; }
        public static List<Game> Games { get => games; set => games = value; }
        public static Dictionary<int, User> ConnectedUsers { get => connectedUsers; set => connectedUsers = value; }
        #endregion

        #region Main Function

        public static void Main(String[] args)
        {
            MythsServer server = new MythsServer();
        }
        #endregion

        #region Constructor

        public MythsServer()
        {
            ConnectedUsers = new Dictionary<int, User>();
            MainQueue = new Queue();
            Games = new List<Game>();

            listener = new TcpListener(IPAddress.Any, 1301);
            listener.Start();

            clientConnection = new Thread(new ThreadStart(ListeningConnection));
            clientConnection.IsBackground = true;
            clientConnection.Start();

            for(; ; )
            { Thread.Sleep(10000); }

        }

        #endregion

        #region Listening Threads Methods

        /**
         * ListeningConnection
         * Listens to new connections to the server
         * (used by a listening thread only)
         */
        public void ListeningConnection()
        {
            while (true)
            {
                Console.WriteLine("Waiting for a connection.");
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client accepted.");

                MythsServer.ConnectedUsers.Add(User.PoolCount + 1, new User(client));

            }
        }

        #endregion

        #region Communication Methods
        //TODO autres types de messages (combat)

        /**
         * SendMessage (authMessage overload)
         * Send a message to the user in parameter
         * @param user : user to send the message
         * @param messageType : Type of the message (Auth/Combat...)
         * @param authMessageType : Type of auth message (LogIn/Logout...)
         */
        public static void SendMessage(User user,Message message)
        {


            //Send Message
            try
            {
                user.ClientStream.Write(message.GetBytes(),0,256);
                Thread.Sleep(10);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e);
            }


        }
        #endregion

        #region Interaction Methods

        /**
         * UserJoinQueue
         * Add a user to the main queue
         * @param user : user to add in queue
         */
        public static void UserJoinQueue(User user)
        {
            MainQueue.AddUser(user);
        }

        /**
         * UserJoinQueue
         * removes a user from the main queue
         * @param user : user to remove from the queue
         */
        public static void UserLeftQueue(User user)
        {
            MainQueue.RemoveUser(user);
        }

        #endregion

    }
}