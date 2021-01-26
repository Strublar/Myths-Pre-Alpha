using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    /**
     * -----Event-----
     * Abstract Class
     * (TODO)
     */
    class Event
    {
        public static int nextEventId = 0;

        #region Attributes
        protected int id;
        protected int targetId;
        protected int sourceId;
        protected Context context;


        #endregion

        #region Getters & Setters
        public int Id { get => id; set => id = value; }
        public int TargetId { get => targetId; set => targetId = value; }
        public int SourceId { get => sourceId; set => sourceId = value; }
        public Context Context { get => context; set => context = value; }
        #endregion

        #region Constructor
        public Event(int targetId, int sourceId)
        {
            this.id = Event.GetNextId();
            this.targetId = targetId;
            this.sourceId = sourceId;
            this.context = null;
        }

        public Event(int targetId, int sourceId, Context context)
        {
            this.id = Event.GetNextId();
            this.targetId = targetId;
            this.sourceId = sourceId;
            this.context = context;

        }
        #endregion

        #region Static Methods
        public static int GetNextId()
        {
            //TODO TO BE TESTED

            return ++Event.nextEventId;
        }
        #endregion

        
        #region Methods

        #endregion
    }
}
