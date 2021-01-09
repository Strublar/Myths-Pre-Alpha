using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class DeathRule : Rule
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
            
            if(newEvent is EntityStatChangedEvent statEvent)
            {
                if(statEvent.StatId == Stat.hp && statEvent.NewValue <= 0)
                {
                    
                    Console.WriteLine("Death rule activated");
                    fightHandler.FireEvent(new EntityStatChangedEvent(newEvent.TargetId,newEvent.TargetId,Stat.isDead,1));
                    fightHandler.FireEvent(new EntityStatChangedEvent(newEvent.TargetId, newEvent.TargetId, Stat.isCalled, 0));
                    fightHandler.FireEvent(new EntityDieEvent(newEvent.TargetId, newEvent.TargetId));

                    Unit target = (Unit)fightHandler.Entities[newEvent.TargetId];
                    fightHandler.FireEvent(new EntityStatChangedEvent(target.Owner.Id, target.Owner.Id, Stat.calls, 
                        target.Owner.GetStat(Stat.calls)+1));


                    //Win condition
                    int deadCounter = 0;
                    foreach(Entity entity in fightHandler.Entities.Values)
                    {
                        if(entity is Myth)
                        {
                            if(entity.Team == fightHandler.Entities[newEvent.TargetId].Team &&
                                entity.GetStat(Stat.isDead) == 1)
                            {
                                deadCounter++;
                            }
                        }
                    }
                    if(deadCounter >= 3)
                    {
                        Console.WriteLine("Game ended");
                        //TODO vainqueur
                        Unit deadUnit = (Unit)fightHandler.Entities[newEvent.TargetId];
                        fightHandler.FireEvent(new EndGameEvent(deadUnit.Owner.Id,
                            deadUnit.Owner.Id));
                    }
                }
            }
        }

        public override void OnAfterEvent(Event newEvent, FightHandler fightHandler)
        {

            if (newEvent is EntityDieEvent dieEvent)
            {
                List<ListeningEffect> purgingList = new List<ListeningEffect>();
                foreach (ListeningEffect listeningEffect in fightHandler.ListeningEffects)
                {
                    if(listeningEffect.HolderId == newEvent.TargetId)
                    {
                        purgingList.Add(listeningEffect);
                    }
                }

                foreach(ListeningEffect listeningEffect in purgingList)
                {
                    fightHandler.ListeningEffects.Remove(listeningEffect);
                }

            }

        }
        #endregion
    }
}
