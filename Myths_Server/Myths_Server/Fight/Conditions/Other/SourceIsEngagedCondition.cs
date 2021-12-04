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
    class SourceIsEngagedCondition : Condition
    {

        #region Attributes
        #endregion

        #region Getters & Setters
        #endregion

        #region Constructor
        public SourceIsEngagedCondition()
        {
        }


        #endregion

        #region Methods
        public override bool IsValid(int targetId, Context context)
        {
            Unit source = (Unit)context.FightHandler.Entities[context.SourceId];


            if (source.GetStat(Stat.isEngaged) == 1)
            {
                return true;
            }
                
            return false;

        }
        #endregion
    }
}
