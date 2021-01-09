
﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    /**
     * -----Entity-----
     * Abstract class that contains the definition of an Entity
     */
    class Entity
    {
        public static int nextId = 0;
        #region Attributes
        private int id;
        private int team;
        private Dictionary<Stat, int> stats;
        private EntityDefinition definition;
        private List<Event> events;

        #endregion

        #region Getters & Setters
        public int Id { get => id; set => id = value; }
        public Dictionary<Stat, int> Stats { get => stats; set => stats = value; }
        public EntityDefinition Definition { get => definition; set => definition = value; }
        public List<Event> Events { get => events; set => events = value; }
        public int Team { get => team; set => team = value; }
        #endregion

        #region Constructor
        public Entity(FightHandler fightHandler, EntityDefinition definition, Dictionary<Stat, int> stats, int team)
        {
            this.events = new List<Event>();
            this.stats = stats;
            this.definition = definition;
            this.id = GetNextId();
            fightHandler.Entities.Add(this.Id, this);
            this.team = team;
        }
        public Entity(FightHandler fightHandler,EntityDefinition definition, int team)
        {
            this.events = new List<Event>();
            this.definition = definition;
            this.stats = this.definition.BaseStats;
            this.id = GetNextId();
            fightHandler.Entities.Add(this.Id, this);
            this.team = team;

        }

        public Entity(FightHandler fightHandler)
        {
            this.events = new List<Event>();
            this.id = GetNextId();
            fightHandler.Entities.Add(this.Id, this);

        }
        #endregion

        #region Static Methods
        public static int GetNextId()
        {
            //TO BE TESTED
            return Entity.nextId++;
        }
        #endregion

        #region Methods

        public int GetStat(Stat statId)
        {
            int output = 0;
            stats.TryGetValue(statId, out output);
            foreach(Event entityEvent in events)
            {
                if(entityEvent is EntityStatChangedEvent statEvent)
                {
                    if(statEvent.StatId == statId)
                    {
                        output = statEvent.NewValue;
                    }
                }
            }
            return output;
        }

        #endregion
    }
}
