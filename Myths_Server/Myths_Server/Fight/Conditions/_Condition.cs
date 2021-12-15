using Myths_Library;
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
    public class Condition
    {

        #region Attributes
        private ConditionDefinition definition;
        private TargetSelector targets;
        private int value;
        #endregion

        #region Getters & Setters
        public int Value { get => value; set => this.value = value; }
        public ConditionDefinition Definition { get => definition; set => definition = value; }
        public TargetSelector Targets { get => targets; set => targets = value; }
        #endregion

        #region Constructor
        public Condition()
        {
        }

        public Condition(ConditionDefinition definition)
        {
            this.definition = definition;
            targets = TargetSelectorBuilder.BuildFrom(definition.selector);
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
