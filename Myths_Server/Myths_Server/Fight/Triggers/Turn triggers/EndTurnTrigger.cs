using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EndTurnTrigger : Trigger
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
            if (newEvent is EndTurnEvent endEvent)
            {
                
                Unit effectHolder = (Unit)(context.FightHandler.Entities[context.HolderId]);
                if (effectHolder.Owner.Id == endEvent.SourceId)
                {
                    return true;
                }
            }
            
            return false;
        }

        #endregion

    }
}
