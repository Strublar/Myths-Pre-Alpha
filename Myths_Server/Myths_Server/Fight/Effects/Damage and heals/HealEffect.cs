using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class HealEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public HealEffect() : base()
        {
        }

        public HealEffect(EffectDefinition definition) 
            : base(definition)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine("healing " + definition.amount + " hp  to " + fightHandler.Entities[targetId].Name);
            //check Full life
            Entity target = fightHandler.Entities[targetId];
            if (target.GetStat(Stat.hp) < target.Stats[Stat.hp])
            {
                //Not full health
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.hp,
                    (int)MathF.Min(target.Stats[Stat.hp], target.GetStat(Stat.hp) + definition.amount)));
            }


        }
        #endregion
    }
}
