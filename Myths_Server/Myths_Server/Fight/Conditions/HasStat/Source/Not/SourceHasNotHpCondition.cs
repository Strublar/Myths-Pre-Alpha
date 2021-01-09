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
    class SourceHasNotHpCondition : Condition
    {

        #region Attributes
        #endregion

        #region Getters & Setters
        #endregion

        #region Constructor
        public SourceHasNotHpCondition()
        {
        }

        
        #endregion

        #region Methods
        public override bool IsValid(Context context)
        {

            Unit source = (Unit)context.FightHandler.Entities[context.SourceId];
            if(source.GetStat(Stat.hp) < Value)
            {
                return true;
            }
            
            return false;

        }
        #endregion
    }
}
