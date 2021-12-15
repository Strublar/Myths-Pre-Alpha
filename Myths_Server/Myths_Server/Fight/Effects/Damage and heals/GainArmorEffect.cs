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

        public GainArmorEffect(EffectDefinition definition) 
            : base(definition)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine("Giving " + definition.amount + " armor  to " + fightHandler.Entities[targetId].Name);
            //check Full armor
            Entity target = fightHandler.Entities[targetId];
            if (target.GetStat(Stat.armor) < target.Stats[Stat.armor])
            {
                //Not full armor
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.armor,
                    (int)MathF.Min(target.Stats[Stat.armor], target.GetStat(Stat.armor) + definition.amount)));
            }


        }
        #endregion
    }
}
