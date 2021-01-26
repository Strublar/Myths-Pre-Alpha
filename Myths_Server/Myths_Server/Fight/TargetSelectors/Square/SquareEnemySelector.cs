using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Myths_Server
{
    class SquareEnemySelector : TargetSelector
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
                               ent.GetStat(Stat.x) >= context.X-1 && ent.GetStat(Stat.x) <= context.X + 1 &&
                               ent.GetStat(Stat.y) >= context.Y - 1 && ent.GetStat(Stat.y) <= context.Y + 1 &&
                               ent.Team != context.FightHandler.Entities[context.SourceId].Team
                               select ent.Id).ToArray() ;
            return targetIds;
        }
        #endregion
    }
}
