using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class GainAttackEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public GainAttackEffect() : base()
        {
        }

        public GainAttackEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" Gains "+value+" attack");

            Entity target = fightHandler.Entities[targetId];
            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                Stat.attack, target.GetStat(Stat.attack) + value));

        }
        #endregion
    }
}
