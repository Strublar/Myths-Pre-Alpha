using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class HealPerMasteryEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public HealPerMasteryEffect() : base()
        {
        }

        public HealPerMasteryEffect(TargetSelector sources, TargetSelector targets, List<int> values) 
            : base(sources, targets, values)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            int element = values.Count > 2 ? values[2] : 0;
            int computedValue = values[0];
            Entity source = fightHandler.Entities[context.SourceId];
            if (source.GetStat(Stat.mastery1) == element ||
                (source.GetStat(Stat.mastery1) != 0 && element == -1))
            {
                computedValue += values[1];
            }
            if (source.GetStat(Stat.mastery2) == element ||
                (source.GetStat(Stat.mastery2) != 0 && element == -1))
            {
                computedValue += values[1];
            }
            if (source.GetStat(Stat.mastery3) == element ||
                (source.GetStat(Stat.mastery3) != 0 && element == -1))
            {
                computedValue += values[1];
            }

            int isTemp = values.Count > 3 ? values[3] : 0;
            Effect newEffect = new HealEffect(
                sources, targets, new List<int> { computedValue, isTemp });
            newEffect.ExecuteOnTarget(targetId, context, fightHandler);

            /*Console.WriteLine("healing "+ computedValue + " hp  to " + fightHandler.Entities[targetId].Definition.Name);
            //check Full life
            Entity target = fightHandler.Entities[targetId];
            if (target.GetStat(Stat.hp)< target.Stats[Stat.hp])
            {
                //Not full health
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.hp,
                    (int)MathF.Min(target.Stats[Stat.hp], target.GetStat(Stat.hp) + computedValue)));
            }*/

        }
        #endregion
    }
}
