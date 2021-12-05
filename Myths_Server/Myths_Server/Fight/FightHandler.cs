
using Myths_Library;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Myths_Server
{
    /**
     * -----FightHandler-----
     * Fire the Events and process all the actions of the Events
     * Manage what happens in the fight
     */
    class FightHandler
    {

        #region Attributes
        private Dictionary<int, Entity> entities;
        private List<Rule> rules;
        private List<ListeningEffect> listeningEffects;
        private Game game;


        #endregion

        #region Getters & Setters
        public Dictionary<int, Entity> Entities { get => entities; set => entities = value; }
        public List<Rule> Rules { get => rules; set => rules = value; }
        public List<ListeningEffect> ListeningEffects { get => listeningEffects; set => listeningEffects = value; }
        public Game Game { get => game; set => game = value; }
        #endregion

        #region Constructor
        public FightHandler(Game game, TeamSet team1, TeamSet team2)
        {
            this.Game = game;
            //Initialisation
            entities = new Dictionary<int, Entity>();
            rules = new List<Rule>() {
                new SummonRule(),
                new EntityCallRule(),
                new EntityMoveRule(),
                new BeginTurnRule(),
                new FirstSpellOfTurnRule(),
                new EndTurnRule(),
                new EntityRecallRule(),
                new DeathRule(),
                new NeverAloneRule(),
                new EntityCastSpellRule(),
                new CommunicationRule() 
            };
            listeningEffects = new List<ListeningEffect>();
            Console.WriteLine("New Fight Handler created");

            

            Player player1 = new Player(this,0);
            Player player2 = new Player(this,1);
            
            game.InitPlayerEntities(player1.Id, player2.Id);
            Myth myth10 = new Myth(this, team1.myths[0], player1.Team,0, player1);
            Myth myth11 = new Myth(this, team1.myths[1], player1.Team, 1, player1);
            Myth myth12 = new Myth(this, team1.myths[2], player1.Team, 2, player1);
            Myth myth13 = new Myth(this, team1.myths[3], player1.Team, 3, player1);
            Myth myth14 = new Myth(this, team1.myths[4], player1.Team, 4, player1);

            Myth myth20 = new Myth(this, team2.myths[0], player2.Team, 0, player2);
            Myth myth21 = new Myth(this, team2.myths[1], player2.Team, 1, player2);
            Myth myth22 = new Myth(this, team2.myths[2], player2.Team, 2, player2);
            Myth myth23 = new Myth(this, team2.myths[3], player2.Team, 3, player2);
            Myth myth24 = new Myth(this, team2.myths[4], player2.Team, 4, player2) ;

            /*Portal portal1 = new Portal(this, 0);
            Portal portal2 = new Portal(this, 1);*/

            /*FireEvent(new EntityCallEvent(myth10.Id, player1.Id, 0, 2));
            FireEvent(new EntityCallEvent(myth20.Id, player2.Id, 6, 2));*/

            //InitGame(player1, player2);
            
            //FireEvent(new BeginTurnEvent(myth10.Id, myth10.Id));

        }

        
        #endregion

        #region Methods
        public void FireEvent(Event newEvent)
        {
            
            if(newEvent is EntityStatChangedEvent statEvent)
            {
                Console.WriteLine("New Event Fired : " + newEvent.GetType().ToString()+" "+ statEvent.StatId +
                    " on "+ entities[statEvent.TargetId].Name);
            }
            else if (newEvent is ListeningEffectPlacedEvent LEevent)
            {
                Console.WriteLine("New Event Fired : " + newEvent.GetType().ToString() 
                    +" ID is "+LEevent.ListeningEffectId+" on " + entities[LEevent.TargetId].Name);

            }
            else
            {
                Console.WriteLine("New Event Fired : " + newEvent.GetType().ToString());
            }
            StoreEvent(newEvent);
            ApplyEventTrigger(newEvent);
        }

        public void StoreEvent(Event newEvent)
        {
            Entity targetEntity = entities[newEvent.TargetId];
            targetEntity.Events.Add(newEvent);
        }

        public void ApplyEventTrigger(Event newEvent)
        {
            TriggerRuleExecution(newEvent);
            TriggerListeningEffectsExecution(newEvent);
            TriggerRuleAfterExecution(newEvent);
            TriggerListeningEffectsEnd(newEvent);
        }

        public void TriggerRuleExecution(Event newEvent)
        {
            foreach(Rule rule in Rules)
            {
                rule.OnEvent(newEvent, this);
            }
        }

        public void TriggerRuleAfterExecution(Event newEvent)
        {
            foreach (Rule rule in Rules)
            {
                rule.OnAfterEvent(newEvent, this);
            }
        }

        public void TriggerListeningEffectsExecution(Event newEvent)
        {

            List<ListeningEffect> triggeringEffects = new List<ListeningEffect>();
            foreach (ListeningEffect listeningEffect in listeningEffects)
            {
                Context context;
                if(newEvent.Context == null)
                {
                    context = new Context(this,
                        listeningEffect.HolderId,
                        newEvent.SourceId);
                    
                }
                else
                {
                    context = newEvent.Context;
                }
                context.HolderId = listeningEffect.HolderId;
                context.TriggeringEvent = newEvent;

                if (listeningEffect.ShouldTriggerExecution(newEvent, context))
                {
                    Console.WriteLine("Listening Effect Executed");

                    triggeringEffects.Add(listeningEffect);
                }
            }

            foreach(ListeningEffect listeningEffect in triggeringEffects)
            {
                Context context;
                if (newEvent.Context == null)
                {
                    context = new Context(this,
                        listeningEffect.HolderId,
                        newEvent.SourceId);
                    context.X = entities[listeningEffect.HolderId].GetStat(Stat.x);
                    context.Y = entities[listeningEffect.HolderId].GetStat(Stat.y);
                }
                else
                {
                    context = newEvent.Context;
                }
                context.HolderId = listeningEffect.HolderId;
                context.TriggeringEvent = newEvent;
                
                listeningEffect.Execute(newEvent, context, this);
            }
            

        }

        public void TriggerListeningEffectsEnd(Event newEvent)
        {
            List<ListeningEffect> purgingList = new List<ListeningEffect>();
            foreach (ListeningEffect listeningEffect in listeningEffects)
            {
                Context context = new Context(this,
                        listeningEffect.HolderId,
                        newEvent.SourceId);
                context.TriggeringEvent = newEvent;
                if (listeningEffect.ShouldTriggerEnd(newEvent, context))
                {
                    Console.WriteLine("Dumping Listening effect " + listeningEffect.Id + " after event" + newEvent.GetType());
                    purgingList.Add(listeningEffect);
                }
            }
            foreach(ListeningEffect listeningEffect in purgingList)
            {
                listeningEffects.Remove(listeningEffect);
            }
        }
        
        public Entity UnitOnTile(int x, int y)
        {
            Entity returnEntity = null;
            foreach(Entity entity in entities.Values)
            {
                if(entity is Unit)
                {
                    if (entity.GetStat(Stat.x) == x && entity.GetStat(Stat.y) == y &&
                    entity.GetStat(Stat.isCalled) == 1)
                    {
                        returnEntity = entity;
                    }
                }
                

            }
            return returnEntity;

        }

        public void InitGame(Player player1,Player player2)
        {
            //initiative
            player1.Initiative = 0;
            player2.Initiative = 0;
            foreach(Entity ent in entities.Values)
            {
                if(ent is Myth myth)
                {
                    if(myth.Owner == player1)
                    {
                        player1.Initiative += myth.Stats[Stat.hp];
                    }
                    if(myth.Owner == player2)
                    {
                        player2.Initiative += myth.Stats[Stat.hp];
                    }
                }
            }

            if(player1.Initiative<=player2.Initiative)
            {
                game.ChangeCurrentPlayer();
                FireEvent(new EntityStatChangedEvent(player1.Id, player1.Id, Stat.calls, 1));
                FireEvent(new EntityStatChangedEvent(player2.Id, player2.Id, Stat.calls, 2));
                FireEvent(new BeginTurnEvent(player2.Id, player2.Id));
            }
            else
            {
                FireEvent(new EntityStatChangedEvent(player1.Id, player2.Id, Stat.calls, 2));
                FireEvent(new EntityStatChangedEvent(player2.Id, player2.Id, Stat.calls, 1));
                FireEvent(new BeginTurnEvent(player1.Id, player1.Id));
            }
            
        }
        #endregion

    }
}
