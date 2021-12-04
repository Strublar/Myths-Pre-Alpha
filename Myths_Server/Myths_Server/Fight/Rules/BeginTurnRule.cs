﻿using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class BeginTurnRule : Rule
    {
        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor

        #endregion

        #region Methods
        public override void OnEvent(Event newEvent, FightHandler fightHandler)
        {
            
            if(newEvent is BeginTurnEvent)
            {
                
                
                Entity target = fightHandler.Entities[newEvent.SourceId];

                if(target is Unit && target.GetStat(Stat.isCalled) == 1)
                {
                    Console.WriteLine("Begin Turn rule activated for " + fightHandler.Entities[newEvent.SourceId].Definition.Name);
                    fightHandler.FireEvent(new EntityStatChangedEvent(target.Id, target.Id, Stat.canMove, 1));
                    fightHandler.FireEvent(new EntityStatChangedEvent(target.Id, target.Id, Stat.canAttack, 1));
                    if(target.GetStat(Stat.isEngaged) == 0)
                    {
                        fightHandler.FireEvent(new EntityStatChangedEvent(target.Id, target.Id, Stat.canRecall, 1));
                    }
                    
                }
                else if (target is Player)
                {
                    fightHandler.Game.ChangeCurrentPlayer();
                }
            }
        }

        public override void OnAfterEvent(Event newEvent, FightHandler fightHandler)
        {

        }
        #endregion
    }
}
