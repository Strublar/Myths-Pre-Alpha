using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EntityCalledEvent : Event
    {
        #region Attributes
        int x, y;
        #endregion

        #region Getters & Setters
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        #endregion

        #region Constructor
        public EntityCalledEvent(int targetId, int sourceId, int x, int y) : base(targetId, sourceId)
        {
            this.x = x;
            this.y = y;
        }


        #endregion

        #region Methods

        #endregion

    }
}
