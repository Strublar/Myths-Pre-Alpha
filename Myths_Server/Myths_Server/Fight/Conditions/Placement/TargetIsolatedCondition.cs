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
    class TargetIsolatedCondition : Condition
    {

        #region Attributes
        #endregion

        #region Getters & Setters
        #endregion

        #region Constructor
        public TargetIsolatedCondition()
        {
        }


        #endregion

        #region Methods
        public override bool IsValid(int targetId, Context context)
        {
            Unit target = (Unit)context.FightHandler.Entities[targetId];

            int[] targetIds = (from ent in context.FightHandler.Entities.Values
                               where ent is Unit && ent.GetStat(Stat.isCalled) == 1 &&
                               ent.GetStat(Stat.x) >= target.GetStat(Stat.x) - 1 && 
                               ent.GetStat(Stat.x) <= target.GetStat(Stat.x) + 1 &&
                               ent.GetStat(Stat.y) >= target.GetStat(Stat.y) - 1 && 
                               ent.GetStat(Stat.y) <= target.GetStat(Stat.y) + 1 &&
                               ent.Team == target.Team && ent.Id != target.Id
                               select ent.Id).ToArray();

            if (targetIds.Length == 0)
            {
                return true;
            }
                
            return false;

        }
        #endregion
    }
}
