using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class GainArcaneGaugeEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public GainArcaneGaugeEffect() : base()
        {
        }

        public GainArcaneGaugeEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.gaugeArcane,
                fightHandler.Entities[targetId].GetStat(Stat.gaugeArcane) + value));

        }
        #endregion
    }
}
