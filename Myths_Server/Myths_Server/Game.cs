using Myths_Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Myths_Server
{
    public class Game
    {
        #region Attributes
        private List<User> users;

        private FightHandler fightHandler;
        private Thread workerThread;
        private Queue<Action<Message>> workerQueue;
        private Queue<Message> workerQueueParameters;

        //Game variables
        private User currentPlayer;
        private Dictionary<User,int> playerEntities;
        private int deployement = 0;
        private bool gameEnded;

        #endregion

        #region Getters & Setters
        public List<User> Users { get => users; set => users = value; }

        internal FightHandler FightHandler { get => fightHandler; set => fightHandler = value; }
        public Queue<Action<Message>> WorkerQueue { get => workerQueue; set => workerQueue = value; }
        public Queue<Message> WorkerQueueParameters { get => workerQueueParameters; set => workerQueueParameters = value; }
        public User CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
        public Dictionary<User, int> PlayerEntities { get => playerEntities; set => playerEntities = value; }
        public bool GameEnded { get => gameEnded; set => gameEnded = value; }

        #endregion

        #region Constructor
        public Game(List<User> users)
        {
            //Test Region
            this.users = users;

            this.WorkerQueue = new Queue<Action<Message>>();
            this.workerQueueParameters = new Queue<Message>();
            this.PlayerEntities = new Dictionary<User, int>();
            Console.WriteLine("Starting a new Game");

            //communication : notify users they joined the game
            foreach(User user in users)
            {
                user.Game = this;
                MythsServer.SendMessage(user, new MatchFoundMessage());
            }

            CurrentPlayer = users[1];
            fightHandler = new FightHandler(this,users[0].Team, users[1].Team);
            deployement = 0;
            GameEnded = false;
            workerThread = new Thread(() => GameLoop(this));
            workerThread.Start();
        }

        #endregion

        #region Methods

        public void SendMessageToAllUsers(Message message)
        {
            foreach (User user in users)
            {
                MythsServer.SendMessage(user, message);
                //Thread.Sleep(10);
            }
        }

        public void SendMessageToUserIndex(int index,Message message)
        {
            Console.WriteLine("Sending a message to " + index+" name is "+users[index].Username);
            MythsServer.SendMessage(users[index], message);
        }

        public void InitPlayerEntities(int player1Id,int player2Id)
        {
            Console.WriteLine("INIT PLAYER ENTITIES : " + player1Id + " " + player2Id);
            this.PlayerEntities.Add(users[0], player1Id);
            this.PlayerEntities.Add(users[1], player2Id);
        }

        public int GetCurrentPlayerId()
        {
            foreach (KeyValuePair<User, int> pair in PlayerEntities)
            {
                if (pair.Key == currentPlayer)
                {
                    return pair.Value;
                }
            }
            return 0;
        }

        public int GetOtherPlayerId()
        {
            foreach(KeyValuePair<User,int> pair in PlayerEntities)
            {
                if(pair.Key != currentPlayer)
                {
                    return pair.Value;
                }
            }
            return 0;
        }

        public void ChangeCurrentPlayer()
        {
            if(this.currentPlayer == users[0])
            {
                this.currentPlayer = users[1];
            }
            else
            {
                this.currentPlayer = users[0];
            }
        }

        public void EndGame()
        {

            gameEnded = true;
            //workerThread.Join();

        }

        public void GatherStats()
        {
            //Not implemented yet
            string dir = Environment.CurrentDirectory;
            string fileName = "../../../Stats/StatsMyths.csv";
            string path = Path.GetFullPath(fileName, dir);
            throw new NotImplementedException();

        }
        #endregion

        #region Worker thread methods

        public void GameLoop(Game game)
        {

            while (!game.GameEnded)
            {
                if(WorkerQueue.Count>0 && WorkerQueueParameters.Count == WorkerQueue.Count)
                {
                    WorkerQueue.Dequeue().Invoke(WorkerQueueParameters.Dequeue());
                }
                Thread.Sleep(5);
            }
        }

        #endregion

        #region Methods


        public void OnCall(Message message)
        {

            CallMessage mess = message as CallMessage;
                /*SendMessageToAllUsers(new EntityStatChangedMessage(targetId, Stat.isCalled, 1));
                SendMessageToAllUsers(new EntityStatChangedMessage(targetId, Stat.x, x));
                SendMessageToAllUsers(new EntityStatChangedMessage(targetId, Stat.y, y));
                SendMessageToAllUsers(new CallMessage(targetId));*/
            fightHandler.FireEvent(new EntityCallEvent(mess.targetId, mess.playerId, mess.x, mess.y));

            List<int> deploymentOrder = new List<int>
            {

                GetOtherPlayerId(),
                GetCurrentPlayerId(),
                GetOtherPlayerId(),
                GetCurrentPlayerId(),
                GetOtherPlayerId(),
                GetCurrentPlayerId()
            };
            if (mess.isSwitch)
            {
                fightHandler.FireEvent(new EntityStatChangedEvent(mess.targetId, mess.targetId, Stat.canMove, 0));
            }

            #region Deployment
            if (deployement <5)
            {
                
                fightHandler.FireEvent(new BeginTurnEvent(deploymentOrder[deployement], deploymentOrder[deployement], true));
                /*fightHandler.FireEvent(new EntityStatChangedEvent(deploymentOrder[deployement], deploymentOrder[deployement], Stat.masteryEarth, 1));
                fightHandler.FireEvent(new EntityStatChangedEvent(deploymentOrder[deployement], deploymentOrder[deployement], Stat.masteryFire, 1));*/
                Console.WriteLine("Turn to : " + deploymentOrder[deployement]);
                if (GetCurrentPlayerId() != deploymentOrder[deployement])
                    fightHandler.Game.ChangeCurrentPlayer();
                deployement++;
            }
            else if(deployement == 5)
            {
                fightHandler.FireEvent(new EndTurnEvent(deploymentOrder[deployement], deploymentOrder[deployement]));
                Console.WriteLine("Turn to : " + deploymentOrder[deployement]);
                if (GetCurrentPlayerId() != deploymentOrder[deployement])
                    fightHandler.Game.ChangeCurrentPlayer();
                deployement++;
            }
            #endregion

        }

        public void OnCastSpell(Message message)
        {
            /*if (parameters[0] is int casterId &&
                parameters[1] is int spellId &&
                parameters[2] is int x &&
                parameters[3] is int y)
            {
                fightHandler.FireEvent(new EntityCastSpellEvent(casterId, casterId, spellId, x, y));
            }*/
        }

        public void OnEndTurn(Message message)
        {

            fightHandler.FireEvent(new EndTurnEvent(GetCurrentPlayerId(), GetCurrentPlayerId()));
                

        }

        public void OnMove(Message message)
        {

            MoveMessage mess = message as MoveMessage;
            /*SendMessageToAllUsers(new EntityStatChangedMessage(targetId, Stat.x, x));
            SendMessageToAllUsers(new EntityStatChangedMessage(targetId, Stat.y, y));
            SendMessageToAllUsers(new MoveMessage(targetId));*/
            fightHandler.FireEvent(new EntityMoveEvent(mess.targetId, mess.targetId, mess.x, mess.y));
            
        }

        public void OnRecall(Message message)
        {

            /*SendMessageToAllUsers(new EntityStatChangedMessage(targetId, Stat.isCalled, 0));
            SendMessageToAllUsers(new UnCallMessage(targetId));*/
            UnCallMessage mess = message as UnCallMessage;
            fightHandler.FireEvent(new EntityRecallEvent(mess.targetId, mess.playerId));
            
        }
        #endregion

    }
}
