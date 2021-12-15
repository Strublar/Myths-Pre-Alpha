using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    /**
     * -----Effect-----
     * Abstract Class
     * Executed by the listeningEffects and fire new Events
     */
    public class Effect
    {
        #region Attributes
        protected EffectDefinition definition;

        public EffectDefinition Definition { get => definition; set => definition = value; }
        #endregion

        #region Constructor
        public Effect()
        {

        }
        public Effect(EffectDefinition definition)
        {
            this.definition = definition;
        }
        #endregion

       

        #region Methods

        public virtual void ExecuteOnTarget(int targetId,Context context, FightHandler fightHandler)
        {

        }

        #endregion
    }
}
