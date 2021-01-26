using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class FirstSpellOfTurnCastEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public FirstSpellOfTurnCastEffect() : base()
        {
        }

        public FirstSpellOfTurnCastEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {


            Console.WriteLine("First spell of turn cast");
            EntityCastSpellEvent triggeringEvent = (EntityCastSpellEvent)context.TriggeringEvent;
            fightHandler.FireEvent(new FirstSpellOfTurnCastEvent(targetId, targetId,
                triggeringEvent.SpellId));


        }
        #endregion
    }
}
