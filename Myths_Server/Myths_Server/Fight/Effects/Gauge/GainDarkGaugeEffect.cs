using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class GainDarkGaugeEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public GainDarkGaugeEffect() : base()
        {
        }

        public GainDarkGaugeEffect(TargetSelector sources, TargetSelector targets, int value) 
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
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.gaugeDark,
                        fightHandler.Entities[targetId].GetStat(Stat.gaugeDark) + value));
                }
            }
        }
        #endregion
    }
}
