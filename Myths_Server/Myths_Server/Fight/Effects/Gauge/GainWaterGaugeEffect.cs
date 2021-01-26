using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class GainWaterGaugeEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public GainWaterGaugeEffect() : base()
        {
        }

        public GainWaterGaugeEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.gaugeWater,
                fightHandler.Entities[targetId].GetStat(Stat.gaugeWater) + value));

        }
        #endregion
    }
}
