﻿using Myths_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Myths_Server
{
    /**
     * -----Condition-----
     * Abstract Class
     * Determines if an effect should apply 
     */
    class IsSourceCondition : Condition
    {

        #region Attributes
        #endregion

        #region Getters & Setters
        #endregion

        #region Constructor
        public IsSourceCondition()
        {
        }

        public IsSourceCondition(ConditionDefinition definition) : base(definition)
        {
        }


        #endregion

        #region Methods
        public override bool IsValid(int targetId, Context context)
        {

            foreach (int id in Targets.GetTargets(context))
            {
                Entity target = context.FightHandler.Entities[id];

                if (target.Id == context.SourceId)
                {
                    return !Definition.inverse;
                }
            }
                
            return Definition.inverse;

        }
        #endregion
    }
}
