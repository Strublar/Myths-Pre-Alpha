using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class GainMasteryRule : Rule
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
            if (newEvent is FirstSpellOfTurnCastEvent castEvent)
            {
                Console.WriteLine("Gain Mastery rule activated");
                SpellDefinition spellCast = SpellDefinition.BuildFrom(castEvent.SpellId);

                List<Effect> effects = new List<Effect> {
                    new GainMasteryEffect(new EffectHolderSelector(), new EffectHolderSelector(),
                    (int)spellCast.Element)
                };
                ListeningEffect newListeningEffect = new ListeningEffect(castEvent.SourceId, InstantTrigger.GetInstantTrigger(),
                    InstantTrigger.GetInstantTrigger(), effects);
                fightHandler.ListeningEffects.Add(newListeningEffect);
                fightHandler.FireEvent(new ListeningEffectPlacedEvent(castEvent.SourceId, castEvent.SourceId, newListeningEffect.Id));
            }
        }
        #endregion
    }
}
