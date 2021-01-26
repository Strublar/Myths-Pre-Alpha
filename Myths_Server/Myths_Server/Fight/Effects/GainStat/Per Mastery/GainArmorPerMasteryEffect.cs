﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class GainArmorPerMasteryEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public GainArmorPerMasteryEffect() : base()
        {
        }

        public GainArmorPerMasteryEffect(TargetSelector sources, TargetSelector targets, int value) 
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

            Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" Gains "+value+" armor");

            Entity target = fightHandler.Entities[targetId];
            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                Stat.armor, target.GetStat(Stat.armor) + value));
            fightHandler.FireEvent(new GainArmorEvent(targetId, targetId, value));

        }
        #endregion
    }
}
