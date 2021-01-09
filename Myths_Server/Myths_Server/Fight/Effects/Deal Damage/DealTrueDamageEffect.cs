using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class DealTrueDamageEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public DealTrueDamageEffect() : base()
        {
        }

        public DealTrueDamageEffect(TargetSelector sources, TargetSelector targets, int value) 
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
                    Console.WriteLine("Dealing "+value+" true damage to " + fightHandler.Entities[targetId].Definition.Name);
                    //check broken guard
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId, Stat.hp,
                        fightHandler.Entities[targetId].GetStat(Stat.hp) - value));
                    
                    
                }
            }
        }
        #endregion
    }
}
