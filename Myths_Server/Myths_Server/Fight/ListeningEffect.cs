using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    /**
     * -----ListeningEffect-----
     * Managed by the FightHandler and react to Events to execute effects
     * Executes its effect when the executionTrigger is triggered
     * Deleted when the endTrigger is triggered
     */
    class ListeningEffect
    {
        //Factory pool control and ID attribution
        public static int nextId = 0; 

        #region Attributes
        private int id;
        private int holderId;
        private List<Trigger> executionTriggers;
        private List<Trigger> endTriggers;
        private List<Effect> effects;
        private ListeningEffectDefinition definition;
        

        #endregion

        #region Getters & Setters

        public int Id { get => id; set => id = value; }
        public int HolderId { get => holderId; set => holderId = value; }
        public List<Trigger> ExecutionTriggers { get => executionTriggers; set => executionTriggers = value; }
        public List<Trigger> EndTriggers { get => endTriggers; set => endTriggers = value; }
        public List<Effect> Effects { get => effects; set => effects = value; }
        internal ListeningEffectDefinition Definition { get => definition; set => definition = value; }

        #endregion

        #region Constructor


        public ListeningEffect(int holderId, List<Trigger> executionTriggers, List<Trigger> endTriggers, List<Effect> effects)
        {
            this.id = ListeningEffect.GetNextId();
            this.holderId = holderId;
            this.executionTriggers = executionTriggers;
            this.endTriggers = endTriggers;
            this.effects = effects;
            foreach (Trigger executionTrigger in this.executionTriggers)
            {
                executionTrigger.ListeningEffectId = this.id;
            }
            foreach (Trigger endTrigger in this.endTriggers)
            {
                endTrigger.ListeningEffectId = this.id;
            }
                
        }

        public ListeningEffect(int holderId, ListeningEffectDefinition listeningEffectDefinition)
        {
            this.id = ListeningEffect.GetNextId();
            this.holderId = holderId;
            this.executionTriggers = listeningEffectDefinition.ExecutionTriggers;
            this.endTriggers = listeningEffectDefinition.EndTriggers;
            this.effects = listeningEffectDefinition.Effects;
            foreach (Trigger executionTrigger in this.executionTriggers)
            {
                executionTrigger.ListeningEffectId = this.id;
            }
            foreach (Trigger endTrigger in this.endTriggers)
            {
                endTrigger.ListeningEffectId = this.id;
            }
        }

        #endregion

        #region Static Methods
        public static int GetNextId()
        {
            //TO BE TESTED
            return ++ListeningEffect.nextId;
        }
        #endregion

        #region Methods

        public bool ShouldTriggerExecution(Event newEvent, Context context)
        {
            foreach(Trigger executionTrigger in executionTriggers)
            {
                if (executionTrigger.ShouldTrigger(newEvent, context))
                {
                    return true;
                }
            }
            
            return false;
            
        }

        public bool ShouldTriggerEnd(Event newEvent, Context context)
        {
            foreach (Trigger endTrigger in endTriggers)
            {
                if (endTrigger.ShouldTrigger(newEvent, context))
                {
                    return true;
                }
            }
                
            return false;

        }

        public void Execute(Event newEvent, Context context, FightHandler fightHandler)
        {
            foreach(Effect effect in Effects)
            {
                Console.WriteLine("Executing " + effect.Name);
                effect.Execute(context, fightHandler);
            }
        }
        #endregion
    }
}
