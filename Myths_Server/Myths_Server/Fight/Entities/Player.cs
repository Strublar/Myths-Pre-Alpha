
using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Myths_Server
{
    public class Player : Entity
    {
        #region Attributes
        private int initiative;
        #endregion

        #region Getters & Setters
        public int Initiative { get => initiative; set => initiative = value; }

        #endregion

        #region Constructor

        public Player(FightHandler fightHandler, int team) : base(fightHandler)
        {
            this.Team = team;
            InitPlayer(fightHandler);
            
        }
        #endregion

        #region Methods
        public void InitPlayer(FightHandler fightHandler)
        {

            this.Stats = new Dictionary<Stat, int>
            {
                { Stat.calls, 0 },
                { Stat.mana, 0 },
                { Stat.masteryLight, 0 },
                { Stat.masteryWater, 0 },
                { Stat.masteryDark, 0 },
                { Stat.masteryAir, 0 },
                { Stat.masteryFire, 0 },
                { Stat.masteryEarth, 0 }
            };
            this.Name = fightHandler.Game.Users[this.Team].Username;
            Console.WriteLine("Send player " + this.Team + " to " + this.Team + " and " + (1 - this.Team));
            //Thread.Sleep(100);//Warning : Used because messages are not recieved
            fightHandler.Game.SendMessageToUserIndex(this.Team, new InitPlayerMessage(this.Team,this.Id,true,this.Name));
            fightHandler.Game.SendMessageToUserIndex(1- this.Team, new InitPlayerMessage(this.Team, this.Id, false, this.Name));
        }
        #endregion

    }
}
