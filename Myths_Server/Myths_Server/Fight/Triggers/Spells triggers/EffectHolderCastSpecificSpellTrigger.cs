using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EffectHolderCastSpecificSpellTrigger : Trigger
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
                if(castSpellEvent.SpellId == Value)
                {
                    if (castSpellEvent.SourceId == context.HolderId)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
        #endregion

    }
}
