using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class DealMagicalDamageEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion
        
        #region Constructor
        public DealMagicalDamageEffect() : base()
        {
        }

        public DealMagicalDamageEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void Execute(Context context, FightHandler fightHandler)
        {
            if(ConditionValid(context))
            {
                foreach(int targetId in targets.GetTargets(context))
                {
                    Console.WriteLine("Dealing "+value+" magical damage to " + fightHandler.Entities[targetId].Definition.Name);
                    //check broken guard
                    if(fightHandler.Entities[targetId].GetStat(Stat.armor) <= 0 ||
                        fightHandler.Entities[targetId].GetStat(Stat.barrier) <= 0)//Guard broken
                    {
                        fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.hp,
                            fightHandler.Entities[targetId].GetStat(Stat.hp) - value));
                    }
                    else if (fightHandler.Entities[targetId].GetStat(Stat.barrier) < value)
                    {
                        int hpLost = value - fightHandler.Entities[targetId].GetStat(Stat.barrier);
                        fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.barrier, 0));
                        fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.hp,
                            fightHandler.Entities[targetId].GetStat(Stat.hp) - hpLost));
                    }
                    else
                    {
                        fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.barrier,
                            fightHandler.Entities[targetId].GetStat(Stat.barrier) -value));
                    }
                    
                }
            }
        }
        #endregion
    }
}
