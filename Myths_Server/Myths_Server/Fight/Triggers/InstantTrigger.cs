using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class InstantTrigger : Trigger
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
            if(newEvent is ListeningEffectPlacedEvent listeningEffectPlacedEvent)
            {
                if (listeningEffectPlacedEvent.ListeningEffectId == this.ListeningEffectId)
                {
                    return true;
                }
            }
            
            return false;
        }

        public static List<Trigger> GetInstantTrigger()
        {
            return new List<Trigger> { new InstantTrigger() };
        }
        #endregion

    }
}
