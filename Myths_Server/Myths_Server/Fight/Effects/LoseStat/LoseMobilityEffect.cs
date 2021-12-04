using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class LoseMobilityEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public LoseMobilityEffect() : base()
        {
        }

        public LoseMobilityEffect(TargetSelector sources, TargetSelector targets, List<int> values) 
            : base(sources, targets, values)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" loses "+ values[0] + " mobility");

            Entity target = fightHandler.Entities[targetId];
            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                Stat.mobility, target.GetStat(Stat.mobility) - values[0]));

            //Temporary Bonus
            if (values.Count > 1)
            {
                if (values[1] == 1)
                {
                    List<Effect> otherEffect = new List<Effect>{
                        new GainMobilityEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                        new List<int> { values[0]}
                        ) };
                    ListeningEffect gainStatEffect = new ListeningEffect(targetId,
                        new List<Trigger> { new EndTurnTrigger(), new EffectHolderRecallTrigger() },
                        new List<Trigger> { new EndTurnTrigger(), new EffectHolderRecallTrigger() },
                        otherEffect);
                    fightHandler.ListeningEffects.Add(gainStatEffect);

                    fightHandler.FireEvent(
                        new ListeningEffectPlacedEvent(gainStatEffect.HolderId, gainStatEffect.HolderId, gainStatEffect.Id));
                }
            }

        }
        #endregion
    }
}
