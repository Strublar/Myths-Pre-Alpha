using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class Unit : Entity
    {
        #region Attributes
        private Player owner;

        #endregion

        #region Getters & Setters
        public Player Owner { get => owner; set => owner = value; }

        #endregion

        #region Constructor
        public Unit(FightHandler fightHandler,int id,EntityDefinition definition, int team, Player owner) : base(fightHandler,definition,team)
        {
            this.owner = owner;
            this.Stats.Add(Stat.mastery1, 0);
            this.Stats.Add(Stat.mastery2, 0);
            this.Stats.Add(Stat.mastery3, 0);
        }

        #endregion

        #region Methods

        #endregion

    }
}
