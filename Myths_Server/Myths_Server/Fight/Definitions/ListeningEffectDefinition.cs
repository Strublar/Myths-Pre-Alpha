using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Myths_Server
{
    class ListeningEffectDefinition
    {
        #region Attributes
        private int id;
        private List<Trigger> executionTriggers;
        private List<Trigger> endTriggers;
        private List<Effect> effects;

        
        #endregion

        #region Getters & Setters
        public List<Trigger> ExecutionTriggers { get => executionTriggers; set => executionTriggers = value; }
        public List<Trigger> EndTriggers { get => endTriggers; set => endTriggers = value; }
        public List<Effect> Effects { get => effects; set => effects = value; }
        public int Id { get => id; set => id = value; }
        #endregion

        #region Constructor
        public ListeningEffectDefinition()
        {
            effects = new List<Effect>();
        }
        public ListeningEffectDefinition(int id,List<Trigger> executionTriggers, List<Trigger> endTriggers, List<Effect> effects)
        {
            this.id = id;
            this.executionTriggers = executionTriggers;
            this.endTriggers = endTriggers;
            this.effects = effects;
            
        }

        #endregion

        #region Methods

        public static ListeningEffectDefinition BuildFrom(int listeningEffectId)
        {
            string dir = Environment.CurrentDirectory;
            string fileName = "../../../Data/ListeningEffects.csv";
            string path = Path.GetFullPath(fileName, dir);

            TextFieldParser parser = new TextFieldParser(path);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(";");
            while (!parser.EndOfData)
            {
                //Process row
                string[] fields = parser.ReadFields();
                if (fields[0].Equals(listeningEffectId.ToString()))
                {
                    ListeningEffectDefinition newListeningEffectDefinition = new ListeningEffectDefinition();

                    #region parsing exec triggers
                    newListeningEffectDefinition.ExecutionTriggers = new List<Trigger>();
                    for(int i=2;i<8;i+=2)
                    {
                        if(!fields[i].Equals(""))
                        {
                            object newExecTriggerObject = Activator.CreateInstance(Type.GetType("Myths_Server." + fields[i]));
                            if (newExecTriggerObject is Trigger execTrigger)
                            {
                                execTrigger.Value = Int32.Parse(fields[i + 1]);
                                newListeningEffectDefinition.ExecutionTriggers.Add(execTrigger);
                            }
                        }
                        
                    }

                    #endregion

                    #region parsing end triggers
                    newListeningEffectDefinition.EndTriggers = new List<Trigger>();
                    for(int i=8;i<14;i+=2)
                    {
                        if (!fields[i].Equals(""))
                        {
                            object newEndTriggerObject = Activator.CreateInstance(Type.GetType("Myths_Server." + fields[i]));
                            if (newEndTriggerObject is Trigger endTrigger)
                            {
                                endTrigger.Value = Int32.Parse(fields[i + 1]);
                                newListeningEffectDefinition.EndTriggers.Add(endTrigger);
                            }
                        }
                            
                    }
                    
                    #endregion

                    if (fields.Length > 14)
                    {
                        for (int i = 14; i < fields.Length; i ++ )
                        {
                            if(!fields[i].Equals(""))
                            {
                                EffectDefinition effectDefinition = EffectDefinition.BuildFrom(Int32.Parse(fields[i]));
                                Effect newEffect = Effect.BuildFrom(effectDefinition);
                                newListeningEffectDefinition.effects.Add(newEffect);
                            }
                        }
                    }
                    return newListeningEffectDefinition;
                }
            }
            return null;
        }
        #endregion
    }
}
