using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    /**
     * -----Context-----
     * Contains informations of the context where/when an Event is fired
     */
    class Context
    {
        #region Attributes
        private FightHandler fightHandler;
        private int holderId;
        private int sourceId;
        private int x, y, originX, originY;
        private Event triggeringEvent;
        #endregion

        #region Getters & Setters
        public int HolderId { get => holderId; set => holderId = value; }
        public int SourceId { get => sourceId; set => sourceId = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        internal FightHandler FightHandler { get => fightHandler; set => fightHandler = value; }
        internal Event TriggeringEvent { get => triggeringEvent; set => triggeringEvent = value; }
        public int OriginX { get => originX; set => originX = value; }
        public int OriginY { get => originY; set => originY = value; }
        #endregion

        #region Constructor
        public Context(FightHandler fightHandler, int holderId, int sourceId)
        {
            this.fightHandler = fightHandler;
            this.holderId = holderId;
            this.sourceId = sourceId;
        }

        #endregion

        #region Methods

        #endregion
    }
}
