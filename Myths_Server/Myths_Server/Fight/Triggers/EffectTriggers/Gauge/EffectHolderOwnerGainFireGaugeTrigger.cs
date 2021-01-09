using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EffectHolderOwnerGainFireGaugeTrigger : Trigger
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
            if(newEvent is GainFireGaugeEvent)
            {
                if(context.FightHandler.Entities[newEvent.TargetId].Team == 
                    context.FightHandler.Entities[context.HolderId].Team)
                {
                    return true;
                }
            }
            
            return false;
        }
        #endregion

    }
}
