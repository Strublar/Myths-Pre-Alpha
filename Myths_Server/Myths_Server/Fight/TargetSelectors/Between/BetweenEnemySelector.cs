﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Myths_Server
{
    class BetweenEnemySelector : TargetSelector
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
            Entity origin = context.FightHandler.Entities[context.SourceId];
            int minX = Math.Min(context.OriginX,context.X);
            int maxX = Math.Max(context.OriginX, context.X);
            int minY = Math.Min(context.OriginY, context.Y);
            int maxY = Math.Max(context.OriginY, context.Y);

            int[] targetIds = (from ent in context.FightHandler.Entities.Values
                               where ent is Unit && ent.GetStat(Stat.isCalled) == 1 &&
                               ent != origin && ent.Team != origin.Team &&
                               ent.GetStat(Stat.x) >= minX && ent.GetStat(Stat.x) <= maxX &&
                               ent.GetStat(Stat.y) >= minY && ent.GetStat(Stat.y) <= maxY
                               select ent.Id).ToArray();

            return targetIds;
        }
        #endregion
    }
}
