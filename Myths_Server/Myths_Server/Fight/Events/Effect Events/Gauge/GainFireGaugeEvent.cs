using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class GainFireGaugeEvent : Event
    {
        #region Attributes
        private int amount;
        #endregion

        #region Getters & Setters
        public int Amount { get => amount; set => amount = value; }
        #endregion

        #region Constructor
        public GainFireGaugeEvent(int targetId, int sourceId, int amount) : base(targetId, sourceId)
        {
            this.amount = amount;
        }

        


        #endregion

        #region Methods

        #endregion

    }
}
