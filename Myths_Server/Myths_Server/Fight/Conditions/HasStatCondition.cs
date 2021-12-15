using Myths_Library;
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
    class HasStatCondition : Condition
    {

        #region Attributes
        #endregion

        #region Getters & Setters
        #endregion

        #region Constructor
        public HasStatCondition()
        {
        }

        public HasStatCondition(ConditionDefinition definition) : base(definition)
        {
        }


        #endregion

        #region Methods
        public override bool IsValid(int targetId, Context context)
        {

            foreach (int id in Targets.GetTargets(context))
            {
                Entity target = context.FightHandler.Entities[id];

                if (target.GetStat(Definition.stat) >= Definition.value)
                {
                    return !Definition.inverse;
                }
            }
                
            return Definition.inverse;

        }
        #endregion
    }
}
