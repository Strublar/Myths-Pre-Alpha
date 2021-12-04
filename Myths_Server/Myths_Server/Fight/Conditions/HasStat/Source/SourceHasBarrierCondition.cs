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
    class SourceHasBarrierCondition : Condition
    {

        #region Attributes
        #endregion

        #region Getters & Setters
        #endregion

        #region Constructor
        public SourceHasBarrierCondition()
        {
        }


        #endregion

        #region Methods
        public override bool IsValid(int targetId, Context context)
        {

            Unit source = (Unit)context.FightHandler.Entities[context.SourceId];
            if(source.GetStat(Stat.barrier) >= Value)
            {
                return true;
            }
            
            return false;

        }
        #endregion
    }
}
