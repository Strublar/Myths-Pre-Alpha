using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class DealPhysicalDamagePerMasteryEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public DealPhysicalDamagePerMasteryEffect() : base()
        {
        }

        public DealPhysicalDamagePerMasteryEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void Execute(Context context, FightHandler fightHandler)
        {
            if(ConditionValid(context))
            {
                int computedValue = 0;
                Entity source = fightHandler.Entities[context.SourceId];
                if (source.GetStat(Stat.mastery1) != 0)
                {
                    computedValue += value;
                }
                if (source.GetStat(Stat.mastery2) != 0)
                {
                    computedValue += value;
                }
                if (source.GetStat(Stat.mastery2) != 0)
                {
                    computedValue += value;
                }

                foreach (int targetId in targets.GetTargets(context))
                {
                    Console.WriteLine("Dealing "+ computedValue + " physical damage to " + fightHandler.Entities[targetId].Definition.Name);
                    //check broken guard
                    if(fightHandler.Entities[targetId].GetStat(Stat.armor) <= 0 ||
                        fightHandler.Entities[targetId].GetStat(Stat.barrier) <= 0)//Guard broken
                    {
                        fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.hp,
                            fightHandler.Entities[targetId].GetStat(Stat.hp) - computedValue));
                    }
                    else if (fightHandler.Entities[targetId].GetStat(Stat.armor) < computedValue)
                    {
                        int hpLost = computedValue - fightHandler.Entities[targetId].GetStat(Stat.armor);
                        fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.armor,0));
                        fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.hp,
                            fightHandler.Entities[targetId].GetStat(Stat.hp) - hpLost));
                    }
                    else
                    {
                        fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.armor,
                            fightHandler.Entities[targetId].GetStat(Stat.armor)- computedValue));
                    }
                    
                }
            }
        }
        #endregion
    }
}
