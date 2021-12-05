using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class SummonRule : Rule
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
            if (newEvent is EntityStatChangedEvent statEvent)
            {
                if (statEvent.StatId == Stat.isCalled && statEvent.NewValue == 1)
                {

                    Entity summonedEntity = fightHandler.Entities[newEvent.TargetId];
                    Console.WriteLine("SummonRule executed for " + summonedEntity.Name);
                    if (summonedEntity != null)
                    {
                        /*foreach (ListeningEffectDefinition listeningEffectDefinition
                            in summonedEntity.Definition.BaseListeningEffects)
                        {
                            ListeningEffect newListeningEffect =
                                new ListeningEffect(newEvent.TargetId, listeningEffectDefinition);
                            fightHandler.ListeningEffects.Add(newListeningEffect);
                            fightHandler.FireEvent(new ListeningEffectPlacedEvent(newEvent.TargetId, newEvent.TargetId,
                                newListeningEffect.Id));
                        }*/
                    }
                }
            }
        }
        #endregion
    }
}
