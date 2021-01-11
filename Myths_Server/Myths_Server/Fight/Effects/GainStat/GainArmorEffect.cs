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

        public GainArmorEffect(TargetSelector sources, TargetSelector targets, int value) 
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
                    Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" Gains "+value+" armor");

                    Entity target = fightHandler.Entities[targetId];
                    if (target.GetStat(Stat.armor) < target.Stats[Stat.armor])
                    {
                        //Not full health
                        fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.armor,
                            (int)MathF.Min(target.Stats[Stat.armor], target.GetStat(Stat.armor) + value)));
                    }
                    fightHandler.FireEvent(new GainArmorEvent(targetId, targetId, value));
                }
            }
        }
        #endregion
    }
}
