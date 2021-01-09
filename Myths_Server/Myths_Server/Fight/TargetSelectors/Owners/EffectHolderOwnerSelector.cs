using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EffectHolderOwnerSelector : TargetSelector
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
            Unit effectHolder = (Unit)context.FightHandler.Entities[context.HolderId];
            int[] targetIds = { effectHolder.Owner.Id };
            return targetIds;
        }
        #endregion
    }
}
