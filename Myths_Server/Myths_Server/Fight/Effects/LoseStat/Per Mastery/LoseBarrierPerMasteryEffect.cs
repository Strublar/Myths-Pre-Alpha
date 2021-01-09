﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class LoseBarrierPerMasteryEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public LoseBarrierPerMasteryEffect() : base()
        {
        }

        public LoseBarrierPerMasteryEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void Execute(Context context, FightHandler fightHandler)
        {
            if(ConditionValid(context))
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

                foreach (int targetId in targets.GetTargets(context))
                {
                    Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" loses "+value+" barrier");

                    Entity target = fightHandler.Entities[targetId];
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                        Stat.barrier, target.GetStat(Stat.barrier) - value));
                }
            }
        }
        #endregion
    }
}
