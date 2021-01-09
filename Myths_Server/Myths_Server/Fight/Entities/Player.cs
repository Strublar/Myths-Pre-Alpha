
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Myths_Server
{
    class Player : Entity
    {
        #region Attributes
        private string name;
        private int initiative;
        #endregion

        #region Getters & Setters
        public string Name { get => name; set => name = value; }
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
            this.Definition = EntityDefinition.GetPlayerDefinition();
            this.Stats = this.Definition.BaseStats;
            this.name = fightHandler.Game.Users[this.Team].Username;
            Console.WriteLine("Send player " + this.Team + " to " + this.Team + " and " + (1 - this.Team));
            //Thread.Sleep(100);//Warning : Used because messages are not recieved
            fightHandler.Game.SendMessageToUserIndex(this.Team, new InitPlayerMessage(this.Team,this.Id,true,this.name));
            fightHandler.Game.SendMessageToUserIndex(1- this.Team, new InitPlayerMessage(this.Team, this.Id, false, this.name));
        }
        #endregion

    }
}
