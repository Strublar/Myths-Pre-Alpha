using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    /**
     * -----Trigger-----
     * Abstract Class
     * Virtual method that returns a boolean whether the trigger is activated to a specified Event
     */
    class Trigger
    {
        #region Attributes
        private int listeningEffectId;
        private int value;
        #endregion

        #region Getters & Setters
        public int ListeningEffectId { get => listeningEffectId; set => listeningEffectId = value; }
        public int Value { get => value; set => this.value = value; }
        #endregion

        #region Constructor

        #endregion

        #region Methods
        public virtual bool ShouldTrigger(Event newEvent, Context context)
        {
            return true;
        }



        #endregion
    }
}
