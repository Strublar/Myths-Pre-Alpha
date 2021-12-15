using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class DealDamageEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public DealDamageEffect() : base()
        {
        }

        public DealDamageEffect(EffectDefinition definition) 
            : base(definition)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine("Dealing " +definition.amount + " physical damage to " + fightHandler.Entities[targetId].Name);
            //check broken guard
            if (fightHandler.Entities[targetId].GetStat(Stat.armor) <= 0)//Guard broken
            {
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.hp,
                    fightHandler.Entities[targetId].GetStat(Stat.hp) - definition.amount));
            }
            else if (fightHandler.Entities[targetId].GetStat(Stat.armor) < definition.amount)
            {
                int hpLost = definition.amount - fightHandler.Entities[targetId].GetStat(Stat.armor);
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.armor, 0));
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.hp,
                    fightHandler.Entities[targetId].GetStat(Stat.hp) - hpLost));
            }
            else
            {
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.armor,
                    fightHandler.Entities[targetId].GetStat(Stat.armor) - definition.amount));
            }

        }
        #endregion
    }
}
