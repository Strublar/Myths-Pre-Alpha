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
    class SourceHasNotMasteryCondition : Condition
    {

        #region Attributes
        #endregion

        #region Getters & Setters
        #endregion

        #region Constructor
        public SourceHasNotMasteryCondition()
        {
        }

        
        #endregion

        #region Methods
        public override bool IsValid(Context context)
        {

            Unit source = (Unit)context.FightHandler.Entities[context.SourceId];
            if(source.GetStat(Stat.mastery1) == Value)
            {
                return false;
            }
            if (source.GetStat(Stat.mastery2) == Value)
            {
                return false;
            }
            if (source.GetStat(Stat.mastery3) == Value)
            {
                return false;
            }
            return true;

        }
        #endregion
    }
}
