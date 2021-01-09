using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class HealPerMasteryEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public HealPerMasteryEffect() : base()
        {
        }

        public HealPerMasteryEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void Execute(Context context, FightHandler fightHandler)
        {
            if(ConditionValid(context))
            {
                int computedValue = 0;
                Entity source = fightHandler.Entities[context.SourceId];
                if (source.GetStat(Stat.mastery1) != 0)
                {
                    computedValue += value;
                }
                if (source.GetStat(Stat.mastery2) != 0)
                {
                    computedValue += value;
                }
                if (source.GetStat(Stat.mastery2) != 0)
                {
                    computedValue += value;
                }
                value = computedValue;


                foreach (int targetId in targets.GetTargets(context))
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
