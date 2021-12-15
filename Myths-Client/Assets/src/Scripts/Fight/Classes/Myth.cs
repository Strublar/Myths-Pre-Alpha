using Myths_Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

public class Myth : Unit
{
    #region Attributes

    private MythDefinition definition;
    private List<SpellDefinition> spells;
    private List<SpellDefinition> masterySpells;
    private SpellDefinition[] ultimates;
    #endregion

    #region Getters & Setters


    public List<SpellDefinition> Spells { get => spells; set => spells = value; }
    public List<SpellDefinition> MasterySpells { get => masterySpells; set => masterySpells = value; }
    public SpellDefinition[] Ultimates { get => ultimates; set => ultimates = value; }
    public MythDefinition Definition { get => definition; set => definition = value; }
    #endregion

    #region Constructor
    public Myth()
    {

    }
    public Myth(int ownerId, int entityId, MythSet set) : base(ownerId,entityId)
    {
        Definition = BuildDefinition(set.id);
        this.Stats.Add(Stat.energy, 0);
        this.Stats.Add(Stat.isEngaged, 0);
        this.Stats.Add(Stat.canRecall, 0);
        this.Stats.Add(Stat.canUlt1, 1);
        this.Stats.Add(Stat.canUlt2, 1);
        this.Stats.Add(Stat.canUlt3, 1);
        this.Stats.Add(Stat.mana,Definition.mana);
        Stats[Stat.hp] = Definition.hp;
        Stats[Stat.armor] = Definition.armor;
        Stats[Stat.mobility] = Definition.mobility;

        this.Spells = new List<SpellDefinition>();
        this.MasterySpells = new List<SpellDefinition>();
        for (int i = 0; i < 3; i++)
        {
            this.Spells.Add(Definition.spellbook[set.spells[i]]);
            this.MasterySpells.Add(Definition.masterySpellBook[set.spells[i]]);
        }

        this.Ultimates = Definition.ultimates;
    }

    public MythDefinition BuildDefinition(int mythId)
    {
        MythDefinition definition = new MythDefinition();

        string dir = Environment.CurrentDirectory;
        string xml = "/Assets/Resources/Data/Myths/"+mythId+".xml";
        string path = Path.GetFullPath(dir + xml);

        int counter = 0;
        retry:
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MythDefinition));
            FileStream fs = new FileStream(path, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);
            definition = (MythDefinition)serializer.Deserialize(reader);
            fs.Close();
        }
        catch (IOException)
        {
            if(counter < 20)
            {
                Thread.Sleep(5);
                goto retry;
            }
            
        }
        

        return definition;
    }

    #endregion
}

