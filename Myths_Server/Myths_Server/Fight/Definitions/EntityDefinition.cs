using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Myths_Server
{
    class EntityDefinition
    {



        #region Attributes
        private int id;
        private string name;
        private Dictionary<Stat, int> baseStats;
        private List<ListeningEffectDefinition> baseListeningEffects;

        
                #endregion

        #region Getters & Setters
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public Dictionary<Stat, int> BaseStats { get => baseStats; set => baseStats = value; }
        public List<ListeningEffectDefinition> BaseListeningEffects { get => baseListeningEffects; set => baseListeningEffects = value; }
        #endregion

        #region Constructor
        public EntityDefinition(int id, string name, Dictionary<Stat, int> baseStats)
        {
            this.baseListeningEffects = new List<ListeningEffectDefinition>();
            this.id = id;
            this.name = name;
            this.baseStats = baseStats;
        }

        public EntityDefinition()
        {

        }
        #endregion

        #region Methods
        public static EntityDefinition BuildFrom(int newEntityId)
        {
            string dir = Environment.CurrentDirectory;
            string fileName = "../../../Data/Entities.csv";
            string path = Path.GetFullPath(fileName, dir);
            
            TextFieldParser parser = new TextFieldParser(path);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(";");
            while (!parser.EndOfData)
            {
                //Process row
                string[] fields = parser.ReadFields();
                if(fields[0].Equals(newEntityId.ToString()))
                {
                    Console.WriteLine(fields[1]);
                    //Stats Parsing
                    Dictionary<Stat, int> stats = new Dictionary<Stat, int>();

                    //Hp
                    stats.Add(Stat.hp, Int32.Parse(fields[2]));
                    //Armor
                    stats.Add(Stat.armor, Int32.Parse(fields[3]));
                    //Barrier
                    stats.Add(Stat.barrier, Int32.Parse(fields[4]));
                    //Attack
                    stats.Add(Stat.attack, Int32.Parse(fields[5]));
                    //Range
                    stats.Add(Stat.range, Int32.Parse(fields[6]));
                    //Atk type
                    stats.Add(Stat.attackType, Int32.Parse(fields[7]));
                    //Mobility
                    stats.Add(Stat.mobility, Int32.Parse(fields[8]));
                    //Energy
                    stats.Add(Stat.energy, Int32.Parse(fields[9]));

                    //Other non parsed stats
                    //position
                    stats.Add(Stat.x, 0);
                    stats.Add(Stat.y, 0);
                    //Control
                    stats.Add(Stat.canAttack, 0);
                    stats.Add(Stat.canMove, 0);
                    stats.Add(Stat.canRecall, 0);
                    //State
                    stats.Add(Stat.isDead, 0);
                    stats.Add(Stat.isCalled, 0);
                    stats.Add(Stat.isEngaged, 0);
                    stats.Add(Stat.canUlt, 1);
                    EntityDefinition newEntity = new EntityDefinition(newEntityId, fields[1], stats);
                    //Passives Parsing
                    if (!fields[10].Equals(""))
                    {
                        ListeningEffectDefinition callEffect = ListeningEffectDefinition.BuildFrom(Int32.Parse(fields[10]));
                        newEntity.baseListeningEffects.Add(callEffect);
                    }
                    if (!fields[11].Equals(""))
                    {
                        ListeningEffectDefinition passive = ListeningEffectDefinition.BuildFrom(Int32.Parse(fields[11]));
                        newEntity.baseListeningEffects.Insert(0,passive);
                    }
                        
                    
                    return newEntity;

                }
            }

            return null;
            
        }

        public static EntityDefinition GetPlayerDefinition()
        {
            EntityDefinition newEntityDefinition = new EntityDefinition();
            newEntityDefinition.Name = "Player";
            newEntityDefinition.baseListeningEffects = new List<ListeningEffectDefinition>();
            newEntityDefinition.baseStats = new Dictionary<Stat, int>
            {
                { Stat.calls,0 }
            };

            return newEntityDefinition;
        }
        #endregion

        

    }
}


