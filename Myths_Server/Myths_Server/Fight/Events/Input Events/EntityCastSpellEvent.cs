using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EntityCastSpellEvent : Event
    {
        #region Attributes
        int spellId, x, y;
        #endregion

        #region Getters & Setters
        public int SpellId { get => spellId; set => spellId = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        #endregion

        #region Constructor
        public EntityCastSpellEvent(int targetId, int sourceId,int spellId,int x, int y) : base(targetId, sourceId)
        {
            this.SpellId = spellId;
            this.X = x;
            this.Y = y;
        }

        


        #endregion

        #region Methods

        #endregion
    }
}
