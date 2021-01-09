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
    class SourceHasArmorCondition : Condition
    {

        #region Attributes
        #endregion

        #region Getters & Setters
        #endregion

        #region Constructor
        public SourceHasArmorCondition()
        {
        }

        
        #endregion

        #region Methods
        public override bool IsValid(Context context)
        {

            Unit source = (Unit)context.FightHandler.Entities[context.SourceId];
            if(source.GetStat(Stat.armor) >= Value)
            {
                return true;
            }
            
            return false;

        }
        #endregion
    }
}
