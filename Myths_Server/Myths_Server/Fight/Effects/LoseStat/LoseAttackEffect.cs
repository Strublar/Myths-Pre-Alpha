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

        public LoseAttackEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" loses "+value+" attack");

            Entity target = fightHandler.Entities[targetId];
            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                Stat.attack, target.GetStat(Stat.attack) - value));

        }
        #endregion
    }
}
