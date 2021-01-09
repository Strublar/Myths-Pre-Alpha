using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class ConsumeMasteryEvent : Event
    {
        #region Attributes
        private Mastery mastery;
        #endregion

        #region Getters & Setters
        public Mastery Mastery { get => mastery; set => mastery = value; }
        #endregion

        #region Constructor
        public ConsumeMasteryEvent(int targetId, int sourceId, int mastery) : base(targetId, sourceId)
        {
            this.mastery = (Mastery)mastery;
        }

        


        #endregion

        #region Methods

        #endregion

    }
}
