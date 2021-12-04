using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EntityCallRule : Rule
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
            
            if(newEvent is EntityCallEvent callEvent)
            {
                Console.WriteLine("Call rule activated");
                if(fightHandler.UnitOnTile(callEvent.X,callEvent.Y) == null &&
                    fightHandler.Entities[newEvent.SourceId].GetStat(Stat.calls)>0)
                {
                    int calledCounter = 0;
                    foreach (Entity entity in fightHandler.Entities.Values)
                    {
                        if(entity is Myth)
                        {
                            if(entity.GetStat(Stat.isCalled) == 1 && 
                                entity.Team == fightHandler.Entities[newEvent.TargetId].Team)
                            {
                                calledCounter++;
                            }
                        }
                        
                    }
                    if (calledCounter < 3) //No more than 3 rule
                    {
                        fightHandler.FireEvent(new EntityStatChangedEvent(callEvent.TargetId, callEvent.TargetId,
                    Stat.x, callEvent.X));
                        fightHandler.FireEvent(new EntityStatChangedEvent(callEvent.TargetId, callEvent.TargetId,
                            Stat.y, callEvent.Y));
                        
                        fightHandler.FireEvent(new EntityStatChangedEvent(callEvent.TargetId, callEvent.TargetId,
                            Stat.canRecall, 1));
                        fightHandler.FireEvent(new EntityStatChangedEvent(callEvent.TargetId, callEvent.TargetId,
                            Stat.energy, fightHandler.Entities[newEvent.TargetId].Stats[Stat.energy]));
                        fightHandler.FireEvent(new EntityStatChangedEvent(callEvent.TargetId, callEvent.TargetId,
                            Stat.canAttack, 1));
                        fightHandler.FireEvent(new EntityStatChangedEvent(callEvent.TargetId, callEvent.TargetId,
                            Stat.canMove, 1));
                        fightHandler.FireEvent(new EntityStatChangedEvent(callEvent.SourceId, callEvent.SourceId,
                            Stat.calls, fightHandler.Entities[newEvent.SourceId].GetStat(Stat.calls)-1));
                        fightHandler.FireEvent(new EntityStatChangedEvent(callEvent.TargetId, callEvent.TargetId,
                            Stat.isCalled, 1));
                        fightHandler.FireEvent(new EntityCalledEvent(callEvent.TargetId, callEvent.TargetId, callEvent.X, callEvent.Y));
                    }
                    else
                    {
                        Console.WriteLine("No more than three rule activated");
                    }
                }
            }
        }

        public override void OnAfterEvent(Event newEvent, FightHandler fightHandler)
        {

        }
        #endregion
    }
}
