using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EntityRecallRule : Rule
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
            
            if(newEvent is EntityRecallEvent)
            {
                Console.WriteLine("Recall rule activated");
                if(fightHandler.Entities[newEvent.TargetId].GetStat(Stat.isEngaged) == 0)
                {
                    int calledCounter = 0;
                    foreach (Entity entity in fightHandler.Entities.Values)
                    {
                        if (entity is Myth)
                        {
                            if (entity.GetStat(Stat.isCalled) == 1 &&
                                entity.Team == fightHandler.Entities[newEvent.TargetId].Team)
                            {
                                calledCounter++;
                            }
                        }

                    }
                    if (calledCounter >1)//Never alone rule 
                    {
                        Entity target = fightHandler.Entities[newEvent.TargetId];
                        //consume mastery
                        int tmp1 = target.GetStat(Stat.mastery1),
                        tmp2 = target.GetStat(Stat.mastery2),
                        tmp3 = target.GetStat(Stat.mastery3);
                        fightHandler.FireEvent(new EntityStatChangedEvent(newEvent.TargetId, newEvent.TargetId,
                        Stat.mastery1, 0));
                        fightHandler.FireEvent(new EntityStatChangedEvent(newEvent.TargetId, newEvent.TargetId,
                        Stat.mastery2, 0));
                        fightHandler.FireEvent(new EntityStatChangedEvent(newEvent.TargetId, newEvent.TargetId,
                        Stat.mastery3, 0));
                        if (tmp1 != 0)
                            fightHandler.FireEvent(new ConsumeMasteryEvent(target.Id, target.Id, tmp1));
                        if (tmp2 != 0)
                            fightHandler.FireEvent(new ConsumeMasteryEvent(target.Id, target.Id, tmp2));
                        if (tmp3 != 0)
                            fightHandler.FireEvent(new ConsumeMasteryEvent(target.Id, target.Id, tmp3));

                        //State
                        fightHandler.FireEvent(new EntityStatChangedEvent(newEvent.TargetId, newEvent.TargetId,
                        Stat.isCalled, 0));
                        fightHandler.FireEvent(new EntityStatChangedEvent(newEvent.TargetId, newEvent.TargetId,
                            Stat.isEngaged, 1));

                        //Reset armor and barrier
                        fightHandler.FireEvent(new EntityStatChangedEvent(newEvent.TargetId, newEvent.TargetId,
                            Stat.armor, target.Stats[Stat.armor]));
                        fightHandler.FireEvent(new EntityStatChangedEvent(newEvent.TargetId, newEvent.TargetId,
                            Stat.barrier, target.Stats[Stat.barrier]));
                    }
                    else
                    {
                        Console.WriteLine("Never alone rule activated");
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
