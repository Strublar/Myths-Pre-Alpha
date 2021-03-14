using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class DealMagicalDamagePerMasteryEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public DealMagicalDamagePerMasteryEffect() : base()
        {
        }

        public DealMagicalDamagePerMasteryEffect(TargetSelector sources, TargetSelector targets, List<int> values) 
            : base(sources, targets, values)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {
            int element = values.Count > 2 ? values[2] : 0;
            int computedValue = values[0];
            Entity source = fightHandler.Entities[context.SourceId];
            if(source.GetStat(Stat.mastery1) == element || 
                (source.GetStat(Stat.mastery1) != 0 && element == -1))
            {
                computedValue += values[1];
            }
            if (source.GetStat(Stat.mastery2) == element ||
                (source.GetStat(Stat.mastery2) != 0 && element == -1))
            {
                computedValue += values[1];
            }
            if (source.GetStat(Stat.mastery3) == element ||
                (source.GetStat(Stat.mastery3) != 0 && element == -1))
            {
                computedValue += values[1];
            }

            int isTemp = values.Count > 3 ? values[3] : 0;
            Effect newEffect = new DealMagicalDamageEffect(
                sources, targets, new List<int> { computedValue, isTemp });
            newEffect.ExecuteOnTarget(targetId, context, fightHandler);
            /*Console.WriteLine("Dealing "+ computedValue + " magical damage to " + fightHandler.Entities[targetId].Definition.Name);
            //check broken guard
            if(fightHandler.Entities[targetId].GetStat(Stat.armor) <= 0 ||
                fightHandler.Entities[targetId].GetStat(Stat.barrier) <= 0)//Guard broken
            {
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.hp,
                    fightHandler.Entities[targetId].GetStat(Stat.hp) - computedValue));
            }
            else if (fightHandler.Entities[targetId].GetStat(Stat.barrier) < computedValue)
            {
                int hpLost = computedValue - fightHandler.Entities[targetId].GetStat(Stat.barrier);
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.barrier, 0));
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.hp,
                    fightHandler.Entities[targetId].GetStat(Stat.hp) - hpLost));
            }
            else
            {
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.barrier,
                    fightHandler.Entities[targetId].GetStat(Stat.barrier) - computedValue));
            }*/

        }
        #endregion
    }
}
