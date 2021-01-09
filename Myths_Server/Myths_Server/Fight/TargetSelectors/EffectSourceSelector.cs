using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EffectSourceSelector : TargetSelector
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
            int[] targetIds = { context.SourceId };
            return targetIds;
        }
        #endregion
    }
}
