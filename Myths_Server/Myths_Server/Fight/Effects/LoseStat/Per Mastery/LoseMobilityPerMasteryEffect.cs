using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class LoseMobilityPerMasteryEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public LoseMobilityPerMasteryEffect() : base()
        {
        }

        public LoseMobilityPerMasteryEffect(TargetSelector sources, TargetSelector targets, List<int> values) 
            : base(sources, targets, values)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            int element = values.Count > 1 ? values[1] : 0;
            int computedValue = 0;
            Entity source = fightHandler.Entities[context.SourceId];
            if (source.GetStat(Stat.mastery1) == element ||
                (source.GetStat(Stat.mastery1) != 0 && element == -1))
            {
                computedValue += values[0];
            }
            if (source.GetStat(Stat.mastery2) == element ||
                (source.GetStat(Stat.mastery2) != 0 && element == -1))
            {
                computedValue += values[0];
            }
            if (source.GetStat(Stat.mastery3) == element ||
                (source.GetStat(Stat.mastery3) != 0 && element == -1))
            {
                computedValue += values[1];
            }

            int isTemp = values.Count > 2 ? values[2] : 0;
            Effect newEffect = new LoseMobilityEffect(
                sources, targets, new List<int> { computedValue, isTemp });
            newEffect.ExecuteOnTarget(targetId, context, fightHandler);

            /*Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" loses "+computedValue+" mobility");

            Entity target = fightHandler.Entities[targetId];
            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                Stat.mobility, target.GetStat(Stat.mobility) - computedValue));*/

        }
        #endregion
    }
}
