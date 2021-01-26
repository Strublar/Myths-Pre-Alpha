using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class GainFireGaugeEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public GainFireGaugeEffect() : base()
        {
        }

        public GainFireGaugeEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.gaugeFire,
                fightHandler.Entities[targetId].GetStat(Stat.gaugeFire) + value));
            fightHandler.FireEvent(new GainFireGaugeEvent(targetId, targetId, value));

        }
        #endregion
    }
}
