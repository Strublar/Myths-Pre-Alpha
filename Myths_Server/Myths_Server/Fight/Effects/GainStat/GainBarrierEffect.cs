using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class GainBarrierEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public GainBarrierEffect() : base()
        {
        }

        public GainBarrierEffect(TargetSelector sources, TargetSelector targets, List<int> values) 
            : base(sources, targets, values)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" Gains "+ values[0] + " barrier");

            Entity target = fightHandler.Entities[targetId];
            if (target.GetStat(Stat.barrier) < target.Stats[Stat.barrier])
            {
                //Not full health
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.barrier,
                    (int)MathF.Min(target.Stats[Stat.barrier], target.GetStat(Stat.barrier) + values[0])));
            }

            //Temporary Bonus
            if (values.Count > 1)
            {
                if (values[1] == 1)
                {
                    List<Effect> otherEffect = new List<Effect>{
                        new LoseBarrierEffect(new EffectHolderSelector(), new EffectHolderSelector(),
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
