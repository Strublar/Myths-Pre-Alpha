using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class BeginTurnEvent : Event
    {
        #region Attributes

        public bool isDraft;
        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public BeginTurnEvent(int targetId, int sourceId, bool isDraft) : base(targetId, sourceId)
        {
            EventType = GameEventType.beginTurn;
            this.isDraft = isDraft;
        }

        
        #endregion

        #region Methods

        #endregion

    }
}
