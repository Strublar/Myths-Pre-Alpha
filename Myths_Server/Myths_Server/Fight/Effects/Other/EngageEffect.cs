﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EngageEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public EngageEffect() : base()
        {
        }

        public EngageEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine(fightHandler.Entities[targetId].Definition.Name+
                " becomes engaged ");

            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.isEngaged, 1));



        }
        #endregion
    }
}
