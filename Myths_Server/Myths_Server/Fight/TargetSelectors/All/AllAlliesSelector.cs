using Myths_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Myths_Server
{
    class AllAlliesSelector : TargetSelector
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
            int sourceTeam = context.FightHandler.Entities[context.SourceId].Team;
            int[] targetIds = (from ent in context.FightHandler.Entities.Values
                               where ent.Team==sourceTeam && ent.GetStat(Stat.isCalled)==1
                               select ent.Id).ToArray();
            return targetIds;
        }
        #endregion
    }
}
