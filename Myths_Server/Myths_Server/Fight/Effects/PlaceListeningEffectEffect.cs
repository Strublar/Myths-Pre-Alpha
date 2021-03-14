using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class PlaceListeningEffectEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public PlaceListeningEffectEffect() : base()
        {
        }

        public PlaceListeningEffectEffect(TargetSelector sources, TargetSelector targets, List<int> values) 
            : base(sources, targets, values)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine("Placing new listening effect");

            ListeningEffectDefinition leDef = ListeningEffectDefinition.BuildFrom(values[0]);

            ListeningEffect newListeningEffect = new ListeningEffect(targetId, leDef);

            fightHandler.ListeningEffects.Add(newListeningEffect);

            fightHandler.FireEvent(
                new ListeningEffectPlacedEvent(newListeningEffect.HolderId, newListeningEffect.HolderId, newListeningEffect.Id));

        }
        #endregion
    }
}
