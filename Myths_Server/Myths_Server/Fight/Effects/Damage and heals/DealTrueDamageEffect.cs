using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class DealTrueDamageEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public DealTrueDamageEffect() : base()
        {
        }

        public DealTrueDamageEffect(EffectDefinition definition) 
            : base(definition)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine("Dealing " +definition.amount + " true damage to " + fightHandler.Entities[targetId].Name);

            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.armor,
                fightHandler.Entities[targetId].GetStat(Stat.armor) - definition.amount));
            

        }
        #endregion
    }
}
