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
        private Queue<Action<object[]>> workerQueue;
        private Queue<object[]> workerQueueParameters;

        //Game variables
        private User currentPlayer;
        private Dictionary<User,int> playerEntities;
        private int deployement = 0;
        private bool gameEnded;

        #endregion

        #region Getters & Setters
        public List<User> Users { get => users; set => users = value; }

        internal FightHandler FightHandler { get => fightHandler; set => fightHandler = value; }
        public Queue<Action<object[]>> WorkerQueue { get => workerQueue; set => workerQueue = value; }
        public Queue<object[]> WorkerQueueParameters { get => workerQueueParameters; set => workerQueueParameters = value; }
        public User CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
        public Dictionary<User, int> PlayerEntities { get => playerEntities; set => playerEntities = value; }
        public bool GameEnded { get => gameEnded; set => gameEnded = value; }

        #endregion

        #region Constructor
        public Game(List<User> users)
        {
            //Test Region
            this.users = users;

            this.WorkerQueue = new Queue<Action<object[]>>();
            this.workerQueueParameters = new Queue<object[]>();
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
            }
        }

        #endregion

        #region Methods

        public void OnAttack(object[] parameters)
        {
            if(parameters[0] is int attackerId &&
                parameters[1] is int targetId)
            {
                //SendMessageToAllUsers(new AttackMessage(attackerId));
                fightHandler.FireEvent(new EntityAttackEvent(targetId, attackerId));
            }
        }

        public void OnCall(object[] parameters)
        {
            if (parameters[0] is int targetId &&
                parameters[1] is int playerId &&
                parameters[2] is int x &&
                parameters[3] is int y &&
                parameters[4] is bool isSwitch)
            {
                /*SendMessageToAllUsers(new EntityStatChangedMessage(targetId, Stat.isCalled, 1));
                SendMessageToAllUsers(new EntityStatChangedMessage(targetId, Stat.x, x));
                SendMessageToAllUsers(new EntityStatChangedMessage(targetId, Stat.y, y));
                SendMessageToAllUsers(new CallMessage(targetId));*/
                fightHandler.FireEvent(new EntityCallEvent(targetId, playerId, x, y));
                Console.WriteLine("IT IS A SWIIIITCH ??? : " + isSwitch);
                if(isSwitch)
                {
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.canMove, 0));
                }
                if(deployement <1)
                {
                    deployement++;
                    fightHandler.FireEvent(new BeginTurnEvent(GetOtherPlayerId(), GetOtherPlayerId()));
                }

            }
        }

        public void OnCastSpell(object[] parameters)
        {
            if (parameters[0] is int casterId &&
                parameters[1] is int spellId &&
                parameters[2] is int x &&
                parameters[3] is int y)
            {
                fightHandler.FireEvent(new EntityCastSpellEvent(casterId, casterId, spellId, x, y));
            }
        }

        public void OnEndTurn(object[] parameters)
        {
            if(parameters[0] is User user)
            {
                if (user == currentPlayer)
                {
                    fightHandler.FireEvent(new EndTurnEvent(GetCurrentPlayerId(), GetCurrentPlayerId()));
                }
            }
            
        }

        public void OnMove(object[] parameters)
        {
            if (parameters[0] is int targetId &&
                parameters[1] is int x &&
                parameters[2] is int y)
            {
                /*SendMessageToAllUsers(new EntityStatChangedMessage(targetId, Stat.x, x));
                SendMessageToAllUsers(new EntityStatChangedMessage(targetId, Stat.y, y));
                SendMessageToAllUsers(new MoveMessage(targetId));*/
                fightHandler.FireEvent(new EntityMoveEvent(targetId,targetId, x, y));
            }
        }

        public void OnRecall(object[] parameters)
        {
            if (parameters[0] is int targetId &&
                parameters[1] is int playerId)
            {
                /*SendMessageToAllUsers(new EntityStatChangedMessage(targetId, Stat.isCalled, 0));
                SendMessageToAllUsers(new UnCallMessage(targetId));*/
                fightHandler.FireEvent(new EntityRecallEvent(targetId, playerId));
            }
        }
        #endregion

    }
}
