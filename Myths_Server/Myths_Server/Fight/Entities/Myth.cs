
using Myths_Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Myths_Server
{
    class Myth : Unit
    {
        #region Attributes
        private Mastery[] elements;
        private List<SpellDefinition> spells;
        private List<SpellDefinition> masterySpells;
        private SpellDefinition[] ultimates;

        #endregion

        #region Getters & Setters
        public Mastery[] Elements { get => elements; set => elements = value; }
        public List<SpellDefinition> Spells { get => spells; set => spells = value; }
        public List<SpellDefinition> MasterySpells { get => masterySpells; set => masterySpells = value; }
        public SpellDefinition[] Ultimates { get => ultimates; set => ultimates = value; }



        #endregion

        #region Constructor


        public Myth(FightHandler fightHandler, MythSet set, int team, byte teamIndex, Player owner) : 
            base(fightHandler,team, owner)
        {
            InitMyth(fightHandler, teamIndex, set);
        }
        #endregion

        #region Methods
        public void InitMyth(FightHandler fightHandler, byte teamIndex, MythSet set)
        {
            MythDefinition definition = BuildDefinition(set.id);
            this.Definition = definition;
            this.Name = definition.name;
            this.Elements = definition.elements;

            this.Stats = new Dictionary<Stat, int>
            {
                {Stat.hp,definition.hp },
                {Stat.armor,definition.armor },
                {Stat.mobility,definition.mobility },
                {Stat.mana,definition.mana },
                {Stat.energy,definition.energy },

                {Stat.x,0 },
                {Stat.y,0 },

                {Stat.canMove,0 },
                {Stat.canRecall,0 },
                {Stat.canUlt1,0 },
                {Stat.canUlt2,0 },
                {Stat.canUlt3,0 },
                {Stat.isCalled,0 },
                {Stat.isDead,0 },
                {Stat.isEngaged,0 },
            };

            this.Spells = new List<SpellDefinition>();
            this.masterySpells = new List<SpellDefinition>();
            for (int i=0;i<3;i++)
            {
                this.Spells.Add(definition.spellbook[set.spells[i]]);
                this.MasterySpells.Add(definition.masterySpellBook[set.spells[i]]);
            }

            this.Ultimates = definition.ultimates;


            fightHandler.Game.SendMessageToAllUsers(new InitMythMessage(Team, teamIndex,Id, set));
        }

        public MythDefinition BuildDefinition(int mythId)
        {
            MythDefinition definition = new MythDefinition();

            //Test Zone
            string path = "../../../Data/Myths/"+ mythId + ".xml";

            XmlSerializer serializer = new XmlSerializer(typeof(MythDefinition));
            FileStream fs = new FileStream(path, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);
            definition = (MythDefinition)serializer.Deserialize(reader);
            fs.Close();

            return definition;
        }
        #endregion
    }
}
