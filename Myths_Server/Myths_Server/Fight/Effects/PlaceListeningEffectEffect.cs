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

        public PlaceListeningEffectEffect(TargetSelector sources, TargetSelector targets, int value) 
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
                    Console.WriteLine("Placing new listening effect");

                    ListeningEffectDefinition leDef = ListeningEffectDefinition.BuildFrom(value);

                    ListeningEffect newListeningEffect = new ListeningEffect(targetId, leDef);

                    fightHandler.ListeningEffects.Add(newListeningEffect);

                    fightHandler.FireEvent(
                        new ListeningEffectPlacedEvent(newListeningEffect.HolderId, newListeningEffect.HolderId, newListeningEffect.Id));


                }
            }
        }
        #endregion
    }
}
