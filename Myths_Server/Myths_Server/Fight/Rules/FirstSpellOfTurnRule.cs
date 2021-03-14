using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class FirstSpellOfTurnRule : Rule
    {
        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor

        #endregion

        #region Methods
        public override void OnEvent(Event newEvent, FightHandler fightHandler)
        {
            
            

            

            
        }

        public override void OnAfterEvent(Event newEvent, FightHandler fightHandler)
        {
            if (newEvent is EntityCastSpellEvent castEvent)
            {
                int counter = 0;
                foreach (Event ev in fightHandler.Entities[newEvent.SourceId].Events)
                {
                    if (ev is EntityCastSpellEvent)
                    {
                        counter++;

                    }
                    if (ev is BeginTurnEvent || ev is EntityCalledEvent)
                    {
                        counter = 0;
                    }

                }
                if (counter == 1)
                {
                    Console.WriteLine("First spell of turn cast");
                    fightHandler.FireEvent(new FirstSpellOfTurnCastEvent(newEvent.SourceId, newEvent.SourceId, castEvent.SpellId));
                }
            }
        }
        #endregion
    }
}
