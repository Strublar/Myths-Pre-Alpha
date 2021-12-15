using Myths_Library;
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
                if(fightHandler.Entities[newEvent.TargetId].GetStat(Stat.isEngaged) == 0 &&
                    fightHandler.Entities[newEvent.TargetId].GetStat(Stat.canRecall) == 1)
                {
                    //Counting units
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

                        //State
                        fightHandler.FireEvent(new EntityStatChangedEvent(newEvent.TargetId, newEvent.TargetId,
                        Stat.isCalled, 0));
                        fightHandler.FireEvent(new EntityStatChangedEvent(newEvent.TargetId, newEvent.TargetId,
                            Stat.isEngaged, 1));

                        //Reset armor and barrier
                        fightHandler.FireEvent(new EntityStatChangedEvent(newEvent.TargetId, newEvent.TargetId,
                            Stat.armor, target.Stats[Stat.armor]));


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
