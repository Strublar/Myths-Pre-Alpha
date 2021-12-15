using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class GainMasteryEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public GainMasteryEffect() : base()
        {
        }

        public GainMasteryEffect(EffectDefinition definition) 
            : base(definition)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine("Gaining " + definition.amount + " mastery " + definition.element);
            //check Full life
            Entity target = fightHandler.Entities[targetId] as Player;
            if (target == null)
                target = (fightHandler.Entities[targetId] as Unit).Owner;

            Stat stat = FightDefines.GetStatFromMastery(definition.element);
            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, stat, target.GetStat(stat) + definition.amount));


        }
        #endregion
    }
}
