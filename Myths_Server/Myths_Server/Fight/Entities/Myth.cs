
﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class Myth : Unit
    {
        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor


        public Myth(FightHandler fightHandler, int unitId, int team, byte teamIndex, Player owner) : 
            base(fightHandler, unitId, EntityDefinition.BuildFrom(unitId),team, owner)
        {
            InitMyth(fightHandler, teamIndex, unitId);
        }
        #endregion

        #region Methods
        public void InitMyth(FightHandler fightHandler, byte teamIndex, int unitId)
        {
            fightHandler.Game.SendMessageToAllUsers(
                new InitMythMessage(Team, teamIndex, Id, unitId,
                this.GetStat(Stat.hp), this.GetStat(Stat.armor), this.GetStat(Stat.barrier), this.GetStat(Stat.attack),
                this.GetStat(Stat.range), this.GetStat(Stat.attackType), this.GetStat(Stat.mobility)));
        }
        #endregion
    }
}
