using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class ConsumeMasteryEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public ConsumeMasteryEffect() : base()
        {
        }

        public ConsumeMasteryEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" consume  mastery");

            Entity target = fightHandler.Entities[targetId];
            if(target.GetStat(Stat.mastery1)==value)
            {
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.mastery1,
                    target.GetStat(Stat.mastery2)));
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.mastery2,
                    target.GetStat(Stat.mastery3)));
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.mastery3, 0));
                fightHandler.FireEvent(new ConsumeMasteryEvent(targetId, targetId, value));
            }
            else if(target.GetStat(Stat.mastery2) == value)
            {
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.mastery2, 0));
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.mastery2,
                    target.GetStat(Stat.mastery3)));
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.mastery3, 0));
                fightHandler.FireEvent(new ConsumeMasteryEvent(targetId, targetId, value));
            }
            else if (target.GetStat(Stat.mastery3) == value)
            {
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.mastery3, 0));
                fightHandler.FireEvent(new ConsumeMasteryEvent(targetId, targetId, value));
            }
            else
            {
                Console.WriteLine("no Mastery consumed");
            }

        }
        #endregion
    }
}
