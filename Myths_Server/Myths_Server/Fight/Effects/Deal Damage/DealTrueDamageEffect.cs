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

        public DealTrueDamageEffect(TargetSelector sources, TargetSelector targets, List<int> values) 
            : base(sources, targets, values)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine("Dealing "+values[0]+" true damage to " + fightHandler.Entities[targetId].Definition.Name);
            //check broken guard
            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.hp,
                fightHandler.Entities[targetId].GetStat(Stat.hp) - values[0]));

        }
        #endregion
    }
}
