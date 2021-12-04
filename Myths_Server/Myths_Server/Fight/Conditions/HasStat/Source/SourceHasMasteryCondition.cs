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
    class SourceHasMasteryCondition : Condition
    {

        #region Attributes
        #endregion

        #region Getters & Setters
        #endregion

        #region Constructor
        public SourceHasMasteryCondition()
        {
        }


        #endregion

        #region Methods
        public override bool IsValid(int targetId, Context context)
        {

            Unit source = (Unit)context.FightHandler.Entities[context.SourceId];
            if(source.GetStat(Stat.mastery1) == Value)
            {
                return true;
            }
            if (source.GetStat(Stat.mastery2) == Value)
            {
                return true;
            }
            if (source.GetStat(Stat.mastery3) == Value)
            {
                return true;
            }
            return false;

        }
        #endregion
    }
}
