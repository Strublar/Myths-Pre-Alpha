using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EndTurnEvent : Event
    {
        #region Attributes
        #endregion

        #region Getters & Setters
        #endregion

        #region Constructor
        public EndTurnEvent(int targetId, int sourceId) : base(targetId, sourceId)
        {
            EventType = GameEventType.endTurn;
        }


        #endregion

        #region Methods

        #endregion
    }
}
