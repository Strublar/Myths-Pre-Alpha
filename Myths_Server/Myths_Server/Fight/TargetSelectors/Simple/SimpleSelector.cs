using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Myths_Server
{
    class SimpleSelector : TargetSelector
    {
        #region Attributes
        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor

        #endregion

        #region Methods

        public override int[] GetTargets(Context context)
        {

            int[] targetIds = (from ent in context.FightHandler.Entities.Values
                               where ent is Unit && ent.GetStat(Stat.isCalled) == 1 &&
                               Utils.GetDistance(ent.GetStat(Stat.x), ent.GetStat(Stat.y),
                               context.X, context.Y)<= Value &&
                               ent.GetStat(Stat.isCalled)==1
                               select ent.Id).ToArray() ;
            return targetIds;
        }
        #endregion
    }
}
