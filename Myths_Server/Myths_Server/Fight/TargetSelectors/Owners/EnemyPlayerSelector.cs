using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EnemyPlayerSelector : TargetSelector
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
            foreach(Entity entity in context.FightHandler.Entities.Values)
            {
                if(entity is Player && entity.Team != context.FightHandler.Entities[context.SourceId].Team)
                {
                    int[] newTargetIds = { entity.Id };
                    return newTargetIds;
                }
            }
            int[] targetIds = { };
            return targetIds;
        }
        #endregion
    }
}
