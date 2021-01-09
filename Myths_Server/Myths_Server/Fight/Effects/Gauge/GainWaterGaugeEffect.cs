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
        public override void Execute(Context context, FightHandler fightHandler)
        {
            if(ConditionValid(context))
            {
                foreach (int targetId in targets.GetTargets(context))
                {
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.gaugeWater,
                        fightHandler.Entities[targetId].GetStat(Stat.gaugeWater) + value));
                }
            }
        }
        #endregion
    }
}
