using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EffectHolderSelector : TargetSelector
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
            int[] targetIds = { context.HolderId };
            return targetIds;
        }
        #endregion
    }
}
