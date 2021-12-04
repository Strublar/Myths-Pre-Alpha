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

        public HealEffect(TargetSelector sources, TargetSelector targets, List<int> values) 
            : base(sources, targets, values)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine("healing "+ values[0] + " hp  to " + fightHandler.Entities[targetId].Definition.Name);
            //check Full life
            Entity target = fightHandler.Entities[targetId];
            if (target.GetStat(Stat.hp)< target.Stats[Stat.hp])
            {
                //Not full health
                fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.hp,
                    (int)MathF.Min(target.Stats[Stat.hp], target.GetStat(Stat.hp) + values[0])));
            }

        }
        #endregion
    }
}
