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
        public override void Execute(Context context, FightHandler fightHandler)
        {
            if(ConditionValid(context))
            {
                foreach (int targetId in targets.GetTargets(context))
                {
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.gaugeFire,
                        fightHandler.Entities[targetId].GetStat(Stat.gaugeFire) + value));
                    fightHandler.FireEvent(new GainFireGaugeEvent(targetId, targetId, value));
                }
            }
        }
        #endregion
    }
}
