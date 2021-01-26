using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class SwapDefenseEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public SwapDefenseEffect() : base()
        {
        }

        public SwapDefenseEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine(fightHandler.Entities[targetId].Definition.Name+
                " swap its defenses ");

            Entity target = fightHandler.Entities[targetId];
            int tempArmor = target.GetStat(Stat.armor);
            int tempBarrier = target.GetStat(Stat.barrier);

            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.armor, tempBarrier));
            fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.barrier, tempArmor));



        }
        #endregion
    }
}
