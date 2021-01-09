using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    /**
     * -----TargetSelector-----
     * Abstract Class
     * (TODO)
     */
    class TargetSelector
    {
        #region Attributes
        private int value = 0;

        #endregion

        #region Getters & Setters
        public int Value { get => value; set => this.value = value; }

        #endregion

        #region Constructor

        #endregion

        #region Methods
        public virtual int[] GetTargets(Context context)
        {
            int[] returnInt = { };
            return returnInt;
        }
        #endregion

        #region Static Methods

        public static void BuildFrom(string targetSelector, string tsValue)
        {

        }
        #endregion
    }
}
