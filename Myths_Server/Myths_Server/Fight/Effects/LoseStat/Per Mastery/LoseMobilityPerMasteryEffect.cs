﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class LoseMobilityPerMasteryEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public LoseMobilityPerMasteryEffect() : base()
        {
        }

        public LoseMobilityPerMasteryEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            int computedValue = 0;
            Entity source = fightHandler.Entities[context.SourceId];
            if (source.GetStat(Stat.mastery1) != 0)
            {
                computedValue += value;
            }
            if (source.GetStat(Stat.mastery2) != 0)
            {
                computedValue += value;
            }
            if (source.GetStat(Stat.mastery2) != 0)
            {
                computedValue += value;
            }
            value = computedValue;

            Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" loses "+value+" mobility");

            Entity target = fightHandler.Entities[targetId];
            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                Stat.mobility, target.GetStat(Stat.mobility) - value));

        }
        #endregion
    }
}
