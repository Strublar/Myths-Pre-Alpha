using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class EntityRecallEvent : Event
    {
        #region Attributes
        #endregion

        #region Getters & Setters
        #endregion

        #region Constructor
        public EntityRecallEvent(int targetId, int sourceId) : base(targetId, sourceId)
        {
        }


        #endregion

        #region Methods

        #endregion
    }
}
