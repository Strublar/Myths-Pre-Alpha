using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EndGameEvent : Event
    {
        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public EndGameEvent(int targetId, int sourceId) : base(targetId, sourceId)
        {
            EventType = GameEventType.endGame;

        }

        
        #endregion

        #region Methods

        #endregion

    }
}
