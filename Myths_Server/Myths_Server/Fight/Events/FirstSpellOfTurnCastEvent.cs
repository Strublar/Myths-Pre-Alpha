using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class FirstSpellOfTurnCastEvent : Event
    {
        #region Attributes
        private int spellId;
        #endregion

        #region Getters & Setters
        public int SpellId { get => spellId; set => spellId = value; }

        #endregion

        #region Constructor
        public FirstSpellOfTurnCastEvent(int targetId, int sourceId, int spellId) : base(targetId, sourceId)
        {
            this.SpellId = spellId;
        }



        #endregion

        #region Methods

        #endregion

    }
}
