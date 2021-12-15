using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EntityCallEvent : Event
    {
        #region Attributes
        int x, y;
        #endregion

        #region Getters & Setters
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        #endregion

        #region Constructor
        public EntityCallEvent(int targetId, int sourceId,int x, int y) : base(targetId, sourceId)
        {
            EventType = GameEventType.entityCall;
            this.x = x;
            this.y = y;
        }

      
        #endregion

        #region Methods

        #endregion
    }
}
