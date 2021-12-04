using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class GainArmorEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public GainArmorEffect() : base()
        {
        }

        public GainArmorEffect(TargetSelector sources, TargetSelector targets, List<int> values) 
            : base(sources, targets, values)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" Gains "+values[0]+" armor");

            Entity target = fightHandler.Entities[targetId];
            if (target.GetStat(Stat.armor) < target.Stats[Stat.armor])
            {
                //Not full health
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.armor,
                    (int)MathF.Min(target.Stats[Stat.armor], target.GetStat(Stat.armor) + values[0])));
            }
            fightHandler.FireEvent(new GainArmorEvent(targetId, targetId, values[0]));

            //Temporary Bonus
            if (values.Count > 1)
            {
                if (values[1] == 1)
                {
                    List<Effect> otherEffect = new List<Effect>{
                        new LoseArmorEffect(new EffectHolderSelector(), new EffectHolderSelector(),
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
