using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Myths_Server
{
    class NeverAloneRule : Rule
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
            
            if(newEvent is EntityDieEvent)
            {
                
                int calledCounter = 0;
                foreach(Entity entity in fightHandler.Entities.Values)
                {
                    if(entity is Myth)
                    {
                        if(entity.GetStat(Stat.isCalled) == 1 && 
                            entity.Team == fightHandler.Entities[newEvent.TargetId].Team &&
                            entity.GetStat(Stat.isDead) == 0)
                        {
                            calledCounter++;
                        }
                    }
                }
                if(calledCounter == 0)
                {
                    Console.WriteLine("Never Alone rule activated on death");
                    foreach(Entity entity in fightHandler.Entities.Values)
                    {
                        if (entity is Myth)
                        {
                            if(entity.Team == fightHandler.Entities[newEvent.TargetId].Team &&
                                entity.GetStat(Stat.isDead)==0)
                            {
                                Entity player = (from ent in fightHandler.Entities.Values
                                                 where ent is Player && ent.Team == entity.Team
                                                 select ent).ToList()[0];
                                fightHandler.FireEvent(new EntityStatChangedEvent(player.Id,player.Id,Stat.calls,
                                    player.GetStat(Stat.calls)+1));
                                fightHandler.FireEvent(new EntityCallEvent(entity.Id, player.Id,
                                    fightHandler.Entities[newEvent.TargetId].GetStat(Stat.x),
                                    fightHandler.Entities[newEvent.TargetId].GetStat(Stat.y)));
                                break;
                            }
                        }
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
