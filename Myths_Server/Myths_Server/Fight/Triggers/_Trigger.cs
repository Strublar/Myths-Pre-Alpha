using Myths_Library;
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
    public class Trigger
    {
        #region Attributes
        private TriggerDefinition definition;
        private List<Condition> conditions;
        private int listeningEffectId;
        private int value;
        #endregion

        #region Getters & Setters
        public int ListeningEffectId { get => listeningEffectId; set => listeningEffectId = value; }
        public int Value { get => value; set => this.value = value; }
        public TriggerDefinition Definition { get => definition; set => definition = value; }
        public List<Condition> Conditions { get => conditions; set => conditions = value; }
        #endregion

        #region Constructor

        public Trigger()
        {
            Conditions = new List<Condition>();
        }

        public Trigger(TriggerDefinition definition)
        {
            this.definition = definition;
            Conditions = new List<Condition>();
            foreach(ConditionDefinition def in definition.conditions)
            {
                Conditions.Add(ConditionBuilder.BuildFrom(def));
            }
        }

        #endregion

        #region Methods
        public virtual bool ShouldTrigger(Event newEvent, Context context)
        {
            if(newEvent.EventType == definition.eventType)
            {
                if (ConditionValid(newEvent.TargetId, context))
                {
                    return true;
                }
            }
            return false;
        }

        public bool ConditionValid(int targetId, Context context)
        {
            foreach (Condition condition in Conditions)
            {
                if (!condition.IsValid(targetId, context))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}
