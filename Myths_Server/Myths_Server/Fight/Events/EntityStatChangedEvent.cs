using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EntityStatChangedEvent : Event
    {
        #region Attributes
        private Stat statId;
        private int newValue;
        #endregion

        #region Getters & Setters
        public Stat StatId { get => statId; set => statId = value; }
        public int NewValue { get => newValue; set => newValue = value; }
        #endregion

        #region Constructor
        public EntityStatChangedEvent(int targetId, int sourceId, Stat stat, int newValue) : base(targetId, sourceId)
        {
            this.statId = stat;
            this.newValue = newValue;
        }

        
        #endregion

        #region Methods

        #endregion

    }
}
