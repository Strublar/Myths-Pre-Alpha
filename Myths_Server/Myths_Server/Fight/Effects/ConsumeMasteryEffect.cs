using Myths_Library;
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

        public ConsumeMasteryEffect(TargetSelector sources, TargetSelector targets, List<int> values) 
            : base(sources, targets, values)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" consume  mastery");
            int count = values.Count > 1 ? values[1] : 1;
            for (int i = 0; i < count; i++)
            {
                Entity target = fightHandler.Entities[targetId];
                if (target.GetStat(Stat.mastery1) == values[0] && count > 0)
                {
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.mastery1,
                        target.GetStat(Stat.mastery2)));
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.mastery2,
                        target.GetStat(Stat.mastery3)));
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.mastery3, 0));
                    fightHandler.FireEvent(new ConsumeMasteryEvent(targetId, targetId, values[0]));
                }
                else if (target.GetStat(Stat.mastery2) == values[0] && count > 0)
                {
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.mastery2, 0));
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.mastery2,
                        target.GetStat(Stat.mastery3)));
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.mastery3, 0));
                    fightHandler.FireEvent(new ConsumeMasteryEvent(targetId, targetId, values[0]));
                }
                else if (target.GetStat(Stat.mastery3) == values[0] && count > 0)
                {
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.mastery3, 0));
                    fightHandler.FireEvent(new ConsumeMasteryEvent(targetId, targetId, values[0]));
                }
                if (count > 0)
                {
                    Console.WriteLine("no Mastery consumed");
                }
            }

        }
        #endregion
    }
}
