using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    /**
     * -----Rule-----
     * Abstract Class
     * (TODO)
     */
    public class Rule
    {
        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor

        #endregion

        #region Methods
        public virtual void OnEvent(Event newEvent, FightHandler fightHandler)
        {

        }

        public virtual void OnAfterEvent(Event newEvent, FightHandler fightHandler)
        {

        }

        #endregion
    }
}
