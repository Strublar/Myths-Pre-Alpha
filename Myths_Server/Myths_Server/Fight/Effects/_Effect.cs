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
        protected List<int> values;
        protected string name;
        protected TargetSelector targets;
        protected TargetSelector sources;
        protected List<Condition> conditions;
        protected Context effectContext;
        protected bool isAbsolute;

        #endregion

        #region Getters & Setters
        public TargetSelector Targets { get => targets; set => targets = value; }
        public TargetSelector Sources { get => sources; set => sources = value; }
        public List<Condition> Conditions { get => conditions; set => conditions = value; }
        public List<int> Values { get => values; set => this.values = value; }
        public string Name { get => name; set => name = value; }
        public bool IsAbsolute { get => isAbsolute; set => isAbsolute = value; }
        public Context EffectContext { get => effectContext; set => effectContext = value; }
        #endregion

        #region Constructor
        public Effect()
        {

        }
        public Effect(TargetSelector sources, TargetSelector targets, List<int> values)
        {
            this.targets = targets;
            this.sources = sources;
            this.conditions = new List<Condition>();
            this.values = values;
        }
        #endregion

        #region Static Methods

        public static Effect BuildFrom(EffectDefinition effectDefinition)
        {
            object newEffectObject = Activator.CreateInstance(effectDefinition.EffectType);
            if(newEffectObject is Effect newEffect)
            {
                newEffect.targets = effectDefinition.TargetSelector;
                newEffect.sources = effectDefinition.SourceSelector;
                newEffect.values = effectDefinition.Values;
                newEffect.Conditions = effectDefinition.Conditions;
                newEffect.Name = effectDefinition.Name;
                newEffect.isAbsolute = effectDefinition.IsAbsolute;
                return newEffect;
            }
            return null;
        }
        #endregion

        #region Methods

        public void Execute(Context context, FightHandler fightHandler)
        {
            
            foreach (int targetId in targets.GetTargets(context))
            {
                if (ConditionValid(targetId, context))
                {
                    ExecuteOnTarget(targetId, context, fightHandler);
                }
            }
            
        }

        public virtual void ExecuteOnTarget(int targetId,Context context, FightHandler fightHandler)
        {

        }


        public bool ConditionValid(int targetId, Context context)
        {
            foreach(Condition condition in conditions)
            {
                if(!condition.IsValid(targetId, context))
                {
                    return false;
                }
            }
            return true;
        }


        #endregion
    }
}
