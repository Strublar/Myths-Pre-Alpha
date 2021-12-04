using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    /**
     * -----Condition-----
     * Abstract Class
     * Determines if an effect should apply 
     */
    class SourceHasAttackCondition : Condition
    {

        #region Attributes
        #endregion

        #region Getters & Setters
        #endregion

        #region Constructor
        public SourceHasAttackCondition()
        {
        }


        #endregion

        #region Methods
        public override bool IsValid(int targetId, Context context)
        {

            Unit source = (Unit)context.FightHandler.Entities[context.SourceId];
            if(source.GetStat(Stat.attack) >= Value)
            {
                return true;
            }
            
            return false;

        }
        #endregion
    }
}
