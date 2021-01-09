using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    /**
     * -----Effect-----
     * Abstract Class
     * Executed by the listeningEffects and fire new Events
     */
    class Effect
    {
        #region Attributes
        protected int value;
        protected string name;
        protected TargetSelector targets;
        protected TargetSelector sources;
        protected List<Condition> conditions;

        
        #endregion

        #region Getters & Setters
        public TargetSelector Targets { get => targets; set => targets = value; }
        public TargetSelector Sources { get => sources; set => sources = value; }
        public List<Condition> Conditions { get => conditions; set => conditions = value; }
        public int Value { get => value; set => this.value = value; }
        public string Name { get => name; set => name = value; }
        #endregion

        #region Constructor
        public Effect()
        {

        }
        public Effect(TargetSelector sources, TargetSelector targets, int value)
        {
            this.targets = targets;
            this.sources = sources;
            this.conditions = new List<Condition>();
            this.value = value;
        }
        #endregion

        #region Static Methods
        public static void BuildFrom(string serializedEffectType,string effectValue,
            string targetSelector, string tsValue, string sourceSelector, string ssValue)
        {

        }

        public static Effect BuildFrom(EffectDefinition effectDefinition)
        {
            object newEffectObject = Activator.CreateInstance(effectDefinition.EffectType);
            if(newEffectObject is Effect newEffect)
            {
                newEffect.targets = effectDefinition.TargetSelector;
                newEffect.sources = effectDefinition.SourceSelector;
                newEffect.value = effectDefinition.Value;
                newEffect.Conditions = effectDefinition.Conditions;
                newEffect.Name = effectDefinition.Name;
                return newEffect;
            }
            return null;
        }
        #endregion

        #region Methods

        public virtual void Execute(Context context, FightHandler fightHandler)
        {

        }

        public bool ConditionValid(Context context)
        {
            foreach(Condition condition in conditions)
            {
                if(!condition.IsValid(context))
                {
                    return false;
                }
            }
            return true;
        }


        #endregion
    }
}
