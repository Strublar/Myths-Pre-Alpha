﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EffectHolderRecallTrigger : Trigger
    {
        #region Attributes

        #endregion

        #region Getters & Setters
        #endregion

        #region Constructor

        #endregion

        #region Methods
        public override bool ShouldTrigger(Event newEvent, Context context)
        {
            if(newEvent is EntityRecallEvent recallEvent)
            {
                if (recallEvent.TargetId == context.HolderId)
                {
                    return true;
                }
            }
            
            return false;
        }


        #endregion

    }
}
