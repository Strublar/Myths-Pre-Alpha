using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EffectHolderCastSpellTrigger : Trigger
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
            if(newEvent is EntityCastSpellEvent castSpellEvent)
            {
                Console.WriteLine("Ding ding "+context.FightHandler.Entities[castSpellEvent.SourceId].Name +
                    " vs "+ context.FightHandler.Entities[context.HolderId].Name);
                if (castSpellEvent.SourceId == context.HolderId)
                {
                    Console.WriteLine("dong ding");
                    return true;
                }
            }
            
            return false;
        }
        #endregion

    }
}
