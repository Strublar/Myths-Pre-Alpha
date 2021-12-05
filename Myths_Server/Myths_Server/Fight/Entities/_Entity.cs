
using Myths_Library;
using System;
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
        private string name;
        private int team;
        private Dictionary<Stat, int> stats;
        private List<Event> events;

        #endregion

        #region Getters & Setters
        public int Id { get => id; set => id = value; }
        public Dictionary<Stat, int> Stats { get => stats; set => stats = value; }
        public List<Event> Events { get => events; set => events = value; }
        public int Team { get => team; set => team = value; }

        public string Name { get => name; set => name = value; }
        #endregion

        #region Constructor
        public Entity(FightHandler fightHandler, Dictionary<Stat, int> stats, int team)
        {
            this.events = new List<Event>();
            this.stats = stats;
            this.id = GetNextId();
            fightHandler.Entities.Add(this.Id, this);
            this.team = team;
        }
        public Entity(FightHandler fightHandler, int team)
        {
            this.events = new List<Event>();
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
