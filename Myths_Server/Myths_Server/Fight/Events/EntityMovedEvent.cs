using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EntityMovedEvent : Event
    {
        #region Attributes
        private int x, y;
        #endregion

        #region Getters & Setters

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        #endregion

        #region Constructor
        public EntityMovedEvent(int targetId, int sourceId, int x, int y) : base(targetId, sourceId)
        {
            EventType = GameEventType.entityMoved;
            this.x = x;
            this.y = y;
        }



        #endregion

        #region Methods

        #endregion

    }
}
