﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class GainLightGaugeEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public GainLightGaugeEffect() : base()
        {
        }

        public GainLightGaugeEffect(TargetSelector sources, TargetSelector targets, int value) 
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
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.gaugeLight,
                        fightHandler.Entities[targetId].GetStat(Stat.gaugeLight) + value));
                }
            }
        }
        #endregion
    }
}
