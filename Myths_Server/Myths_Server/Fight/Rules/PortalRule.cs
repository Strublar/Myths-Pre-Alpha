using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class PortalRule : Rule
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

                if(target is Player)
                {
                    foreach(Entity entity in fightHandler.Entities.Values)
                    {
                        if(entity is Portal portal && entity.Team == target.Team)
                        {
                            Console.WriteLine("Portal rule on begin turn " + fightHandler.Entities[newEvent.SourceId].Definition.Name);
                            fightHandler.FireEvent(new EntityStatChangedEvent(target.Id, target.Id,Stat.gaugeArcane,
                                target.GetStat(Stat.gaugeArcane)+1));
                        }
                    }
                    
                }

            }

            
        }

        public override void OnAfterEvent(Event newEvent, FightHandler fightHandler)
        {
            if (newEvent is EntityMovedEvent movedEvent)
            {
                Entity target = fightHandler.Entities[newEvent.SourceId];
                foreach (Entity entity in fightHandler.Entities.Values)
                {
                    if (entity is Portal && entity.GetStat(Stat.x) == target.GetStat(Stat.x) &&
                        entity.GetStat(Stat.y) == target.GetStat(Stat.y))
                    {
                        if(entity.Team != target.Team)
                        {
                            Console.WriteLine("Portal capture rule ");
                            entity.Team = target.Team;
                            fightHandler.FireEvent(new CapturePortalEvent(entity.Id, target.Id));
                            fightHandler.FireEvent(new EntityStatChangedEvent(((Unit)target).Owner.Id, 
                                ((Unit)target).Owner.Id, Stat.gaugeArcane,
                                ((Unit)target).Owner.GetStat(Stat.gaugeArcane) + 2));
                        }
                        
                    }
                }
            }
        }
        #endregion
    }
}
