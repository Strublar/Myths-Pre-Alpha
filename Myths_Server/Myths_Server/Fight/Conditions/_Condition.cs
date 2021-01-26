using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    /**
     * -----Condition-----
     * Abstract Class
     * Determines if an effect should apply 
     */
    class Condition
    {

        #region Attributes
        private int value;
        #endregion

        #region Getters & Setters
        public int Value { get => value; set => this.value = value; }
        #endregion

        #region Constructor
        public Condition()
        {
        }

        
        #endregion

        #region Methods
        public virtual bool IsValid(int targetId, Context context)
        {
            //TODO
            return true;
        }
        #endregion
    }
}
