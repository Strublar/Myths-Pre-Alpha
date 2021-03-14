using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class LoseAttackEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public LoseAttackEffect() : base()
        {
        }

        public LoseAttackEffect(TargetSelector sources, TargetSelector targets, List<int> values) 
            : base(sources, targets, values)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" loses "+ values[0] + " attack");

            Entity target = fightHandler.Entities[targetId];
            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                Stat.attack, target.GetStat(Stat.attack) - values[0]));

            //Temporary Bonus
            if (values.Count > 1)
            {
                if (values[1] == 1)
                {
                    List<Effect> otherEffect = new List<Effect>{
                        new GainAttackEffect(new EffectHolderSelector(), new EffectHolderSelector(),
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
