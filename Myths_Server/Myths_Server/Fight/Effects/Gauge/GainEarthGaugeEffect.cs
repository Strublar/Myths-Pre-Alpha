using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class GainEarthGaugeEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public GainEarthGaugeEffect() : base()
        {
        }

        public GainEarthGaugeEffect(TargetSelector sources, TargetSelector targets, int value) 
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
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.gaugeEarth,
                        fightHandler.Entities[targetId].GetStat(Stat.gaugeEarth) + value));
                }
            }
        }
        #endregion
    }
}
