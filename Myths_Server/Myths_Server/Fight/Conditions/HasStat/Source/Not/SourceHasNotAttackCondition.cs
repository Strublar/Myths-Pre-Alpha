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
    class SourceHasNotAttackCondition : Condition
    {

        #region Attributes
        #endregion

        #region Getters & Setters
        #endregion

        #region Constructor
        public SourceHasNotAttackCondition()
        {
        }

        
        #endregion

        #region Methods
        public override bool IsValid(Context context)
        {

            Unit source = (Unit)context.FightHandler.Entities[context.SourceId];
            if(source.GetStat(Stat.attack) < Value)
            {
                return true;
            }
            
            return false;

        }
        #endregion
    }
}
