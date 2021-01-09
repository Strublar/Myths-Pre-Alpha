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

        public HealEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void Execute(Context context, FightHandler fightHandler)
        {
            if(ConditionValid(context))
            {
                foreach(int targetId in targets.GetTargets(context))
                {
                    Console.WriteLine("healing "+value+" hp  to " + fightHandler.Entities[targetId].Definition.Name);
                    //check Full life
                    Entity target = fightHandler.Entities[targetId];
                    if (target.GetStat(Stat.hp)< target.Stats[Stat.hp])
                    {
                        //Not full health
                        fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.hp,
                            (int)MathF.Min(target.Stats[Stat.hp], target.GetStat(Stat.hp) + value)));
                    }
                    
                    
                }
            }
        }
        #endregion
    }
}
