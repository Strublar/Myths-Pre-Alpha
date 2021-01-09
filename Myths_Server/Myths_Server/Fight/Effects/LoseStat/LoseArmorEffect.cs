using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class LoseArmorEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public LoseArmorEffect() : base()
        {
        }

        public LoseArmorEffect(TargetSelector sources, TargetSelector targets, int value) 
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
                    Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" loses "+value+" armor");

                    Entity target = fightHandler.Entities[targetId];
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                        Stat.armor, target.GetStat(Stat.armor) - value));
                }
            }
        }
        #endregion
    }
}
