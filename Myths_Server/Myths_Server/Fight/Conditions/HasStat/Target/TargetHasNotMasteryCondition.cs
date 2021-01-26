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
    class TargetHasNotMasteryCondition : Condition
    {

        #region Attributes
        #endregion

        #region Getters & Setters
        #endregion

        #region Constructor
        public TargetHasNotMasteryCondition()
        {
        }


        #endregion

        #region Methods
        public override bool IsValid(int targetId, Context context)
        {

            Unit target = (Unit)context.FightHandler.Entities[targetId];
            if(target.GetStat(Stat.mastery1) == Value)
            {
                return false;
            }
            if (target.GetStat(Stat.mastery2) == Value)
            {
                return false;
            }
            if (target.GetStat(Stat.mastery3) == Value)
            {
                return false;
            }
            return true;

        }
        #endregion
    }
}
