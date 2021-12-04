using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EndTurnRule : Rule
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
            
            if(newEvent is EndTurnEvent)
            {
                Console.WriteLine("End turn rule activated");
                //+1 call
                fightHandler.FireEvent(new EntityStatChangedEvent(fightHandler.Game.GetCurrentPlayerId(),
                    fightHandler.Game.GetCurrentPlayerId(), Stat.calls,
                    fightHandler.Entities[fightHandler.Game.GetCurrentPlayerId()].GetStat(Stat.calls) + 1));

                //Energy refill
                foreach(Entity entity in fightHandler.Entities.Values)
                {
                    if(entity is Myth myth)
                    {
                        if(myth.GetStat(Stat.isCalled) ==1 && myth.Owner.Id == fightHandler.Game.GetCurrentPlayerId())
                        {
                            fightHandler.FireEvent(new EntityStatChangedEvent(entity.Id, entity.Id, Stat.energy, 
                                entity.Stats[Stat.energy]));
                        }
                    }
                }

                
            }
        }

        public override void OnAfterEvent(Event newEvent, FightHandler fightHandler)
        {
            if (newEvent is EndTurnEvent)
            {
                int currentTeam = fightHandler.Entities[fightHandler.Game.GetOtherPlayerId()].Team;
                foreach (Entity ent in fightHandler.Entities.Values)
                {
                    
                    if (ent.Team == currentTeam && (ent.GetStat(Stat.isCalled) == 1 || ent is Player))
                    {
                        fightHandler.FireEvent(new BeginTurnEvent(ent.Id,ent.Id));
                    }
                }
                
            }
                
        }
        #endregion
    }
}
