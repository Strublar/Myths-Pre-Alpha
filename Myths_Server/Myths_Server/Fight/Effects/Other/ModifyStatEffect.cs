using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class ModifyStatEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public ModifyStatEffect() : base()
        {
        }

        public ModifyStatEffect(EffectDefinition definition) 
            : base(definition)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {


            Console.WriteLine(fightHandler.Entities[targetId].Name + " Gains " + definition.amount + " "+definition.stat);

            Entity target = fightHandler.Entities[targetId];
            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                definition.stat, target.GetStat(definition.stat) + definition.amount));

            //Temporary Bonus

            if (definition.temporary)
            {
                throw new NotImplementedException("Need to implement temporary bonuses");
                /*List<Effect> otherEffect = new List<Effect>{
                    new LoseEnergyEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                    new List<int> { values[0]}
                    ) };
                ListeningEffect removeStatEffect = new ListeningEffect(targetId,
                    new List<Trigger> { new EndTurnTrigger(), new EffectHolderRecallTrigger() },
                    new List<Trigger> { new EndTurnTrigger(), new EffectHolderRecallTrigger() },
                    otherEffect);
                fightHandler.ListeningEffects.Add(removeStatEffect);

                fightHandler.FireEvent(
                    new ListeningEffectPlacedEvent(removeStatEffect.HolderId, removeStatEffect.HolderId, removeStatEffect.Id));*/
            }
            



        }
        #endregion
    }
}
