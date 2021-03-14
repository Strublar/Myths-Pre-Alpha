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
    class SourceHasGaugeCondition : Condition
    {

        #region Attributes
        #endregion

        #region Getters & Setters
        #endregion

        #region Constructor
        public SourceHasGaugeCondition()
        {
        }


        #endregion

        #region Methods
        public override bool IsValid(int targetId, Context context)
        {

            Unit sourceUnit = (Unit)(context.FightHandler.Entities[context.SourceId]);
            Stat gauge = Utils.GetGaugeFromMastery((Mastery)Value);
            if(sourceUnit.Owner.GetStat(gauge) > 0)
            {
                return true;
            }
            
            return false;

        }
        #endregion
    }
}
