﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class LoseCallEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public LoseCallEffect() : base()
        {
        }

        public LoseCallEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void Execute(Context context, FightHandler fightHandler)
        {
            if(ConditionValid(context))
            {
                foreach(int targetId in targets.GetTargets(context))
                {
                    Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" loses "+value+" calls");

                    Entity target = fightHandler.Entities[targetId];
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                        Stat.calls, target.GetStat(Stat.calls) - value));
                }
            }
        }
        #endregion
    }
}
