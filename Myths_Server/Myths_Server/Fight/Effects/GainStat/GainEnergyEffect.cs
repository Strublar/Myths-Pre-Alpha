using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class GainEnergyEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public GainEnergyEffect() : base()
        {
        }

        public GainEnergyEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" Gains "+value+" energy");

            Entity target = fightHandler.Entities[targetId];
            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                Stat.energy, target.GetStat(Stat.energy) + value));

        }
        #endregion
    }
}
