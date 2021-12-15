using Myths_Library;
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
    public class EffectsGroup
    {
        #region Attributes
        protected EffectsGroupDefinition definition;
        protected TargetSelector targets;
        protected List<Condition> conditions;
        protected List<Effect> effects;
        protected Context effectContext;

        #endregion

        #region Getters & Setters
        public TargetSelector Targets { get => targets; set => targets = value; }

        public Context EffectContext { get => effectContext; set => effectContext = value; }
        public List<Effect> Effects { get => effects; set => effects = value; }
        public EffectsGroupDefinition Definition { get => definition; set => definition = value; }
        public List<Condition> Conditions { get => conditions; set => conditions = value; }
        #endregion

        #region Constructor
        public EffectsGroup()
        {

        }
        public EffectsGroup(EffectsGroupDefinition definition)
        {
            this.definition = definition;
            this.effects = new List<Effect>();
            foreach(EffectDefinition def in definition.effects)
            {
                effects.Add(EffectBuilder.BuildFrom(def));
            }
            this.Conditions = new List<Condition>();
            foreach(ConditionDefinition def in definition.conditions)
            {
                conditions.Add(ConditionBuilder.BuildFrom(def));
            }

            targets = TargetSelectorBuilder.BuildFrom(definition.targets);
        }
        #endregion


        #region Methods

        public void Execute(Context context, FightHandler fightHandler)
        {
            
            foreach (int targetId in targets.GetTargets(context))
            {
                if (ConditionValid(targetId, context))
                {
                    foreach(Effect effect in effects)
                    {
                        effect.ExecuteOnTarget(targetId, context, fightHandler);
                    }
                }
            }
            
        }


        public bool ConditionValid(int targetId, Context context)
        {
            foreach(Condition condition in Conditions)
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
