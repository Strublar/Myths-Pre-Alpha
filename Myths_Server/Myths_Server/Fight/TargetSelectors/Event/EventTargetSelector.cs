using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EventTargetSelector : TargetSelector
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
            int[] targetIds = { context.TriggeringEvent.TargetId };
            return targetIds;
        }
        #endregion
    }
}
