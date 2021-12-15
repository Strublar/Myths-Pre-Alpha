using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class ListeningEffectPlacedEvent : Event
    {
        #region Attributes
        private int listeningEffectId;
        #endregion

        #region Getters & Setters
        public int ListeningEffectId { get => listeningEffectId; set => listeningEffectId = value; }
        #endregion

        #region Constructor
        public ListeningEffectPlacedEvent(int targetId, int sourceId, int listeningEffectId) : base(targetId, sourceId)
        {
            EventType = GameEventType.listeningEffectPlaced;
            this. listeningEffectId = listeningEffectId;
        }


        #endregion

        #region Methods

        #endregion
    }
}
