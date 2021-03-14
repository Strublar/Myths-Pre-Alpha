using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class DealPhysicalDamageEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public DealPhysicalDamageEffect() : base()
        {
        }

        public DealPhysicalDamageEffect(TargetSelector sources, TargetSelector targets, List<int> values) 
            : base(sources, targets, values)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId,Context context, FightHandler fightHandler)
        {

            Console.WriteLine("Dealing "+ values[0] + " physical damage to " + fightHandler.Entities[targetId].Definition.Name);
            //check broken guard
            if(fightHandler.Entities[targetId].GetStat(Stat.armor) <= 0 ||
                fightHandler.Entities[targetId].GetStat(Stat.barrier) <= 0)//Guard broken
            {
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.hp,
                    fightHandler.Entities[targetId].GetStat(Stat.hp) - values[0]));
            }
            else if (fightHandler.Entities[targetId].GetStat(Stat.armor) < values[0])
            {
                int hpLost = values[0] - fightHandler.Entities[targetId].GetStat(Stat.armor);
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.armor,0));
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.hp,
                    fightHandler.Entities[targetId].GetStat(Stat.hp) - hpLost));
            }
            else
            {
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.armor,
                    fightHandler.Entities[targetId].GetStat(Stat.armor)- values[0]));
            }
                    
        }
        #endregion
    }
}
