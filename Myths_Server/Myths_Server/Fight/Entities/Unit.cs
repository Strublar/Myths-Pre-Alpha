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
        private List<ListeningEffectDefinition> passives;
        #endregion

        #region Getters & Setters
        public Player Owner { get => owner; set => owner = value; }
        internal List<ListeningEffectDefinition> Passives { get => passives; set => passives = value; }

        #endregion

        #region Constructor
        public Unit(FightHandler fightHandler, int team, Player owner) : base(fightHandler,team)
        {
            this.owner = owner;
            this.Passives = new List<ListeningEffectDefinition>();

        }

        #endregion

        #region Methods

        #endregion

    }
}
