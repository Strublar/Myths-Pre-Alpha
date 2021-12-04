using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class GainRangeEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public GainRangeEffect() : base()
        {
        }

        public GainRangeEffect(TargetSelector sources, TargetSelector targets, List<int> values) 
            : base(sources, targets, values)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" Gains "+ values[0] + " mobility");

            Entity target = fightHandler.Entities[targetId];
            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                Stat.range, target.GetStat(Stat.range) + values[0]));

            //Temporary Bonus
            if(values.Count > 1)
            {
                if(values[1] == 1)
                {
                    List<Effect> otherEffect = new List<Effect>{
                        new LoseRangeEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                        new List<int> { values[0]}
                        ) };
                    ListeningEffect removeStatEffect = new ListeningEffect(targetId, 
                        new List<Trigger> { new EndTurnTrigger(), new EffectHolderRecallTrigger() },
                        new List<Trigger> { new EndTurnTrigger(), new EffectHolderRecallTrigger() },
                        otherEffect);
                    fightHandler.ListeningEffects.Add(removeStatEffect);

                    fightHandler.FireEvent(
                        new ListeningEffectPlacedEvent(removeStatEffect.HolderId, removeStatEffect.HolderId, removeStatEffect.Id));
                }
            }

        }
        #endregion
    }
}
