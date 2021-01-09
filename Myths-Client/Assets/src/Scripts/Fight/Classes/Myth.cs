using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Myth : Unit
{
    #region Attributes

    private List<Spell> spells;
    
    #endregion

    #region Getters & Setters


    public List<Spell> Spells { get => spells; set => spells = value; }
    #endregion

    #region Constructor
    public Myth()
    {

    }
    public Myth(int ownerId, int entityId, int unitId) : base(ownerId,entityId,unitId)
    {
        this.Stats.Add(Stat.energy, 0);
        this.Stats.Add(Stat.isEngaged, 0);
        this.Stats.Add(Stat.canRecall, 0);
        this.Stats.Add(Stat.canUlt, 1);
        spells = new List<Spell>();
        //TODO Change les sorts
        GetSpells(unitId);

        foreach(Spell spell in spells)
        {
            spell.Owner = this;
        }
    }

    public void GetSpells(int unitId)
    {
        string dir = Environment.CurrentDirectory;
        string csv = "/Assets/Resources/Units/Units.csv";
        string path = Path.GetFullPath(dir + csv);
        StreamReader strReader = new StreamReader(path);

        string fileString = strReader.ReadToEnd();
        string[] lines = fileString.Split('\n');
        string[] fields = lines[unitId + 1].Split(';');
        if (fields[0].Equals(unitId.ToString()))
        {
            spells.Add(Spell.ParseSpell(int.Parse(fields[2])));
            spells.Add(Spell.ParseSpell(int.Parse(fields[3])));
            spells.Add(Spell.ParseSpell(int.Parse(fields[4])));
            spells.Add(Spell.ParseSpell(int.Parse(fields[5])));

            //Ults
            spells.Add(Spell.ParseSpell(int.Parse(fields[6])));
            spells.Add(Spell.ParseSpell(int.Parse(fields[7])));
            spells.Add(Spell.ParseSpell(int.Parse(fields[8])));
        }
    }

    public static Myth ParseMyth(int id)
    {
        Myth newMyth = new Myth();
        newMyth.Id = id;
        newMyth.Spells = new List<Spell>();
        //TODO Change les sorts
        newMyth.GetSpells(id);

        foreach (Spell spell in newMyth.Spells)
        {
            spell.Owner = newMyth;
        }

        newMyth.Stats.Add(Stat.hp, 0);
        newMyth.Stats.Add(Stat.armor, 0);
        newMyth.Stats.Add(Stat.barrier, 0);
        newMyth.Stats.Add(Stat.attack, 0);
        newMyth.Stats.Add(Stat.mobility, 2);
        newMyth.Stats.Add(Stat.range, 1);
        newMyth.Stats.Add(Stat.attackType, 1);
        
        newMyth.ParseUnit(id);
        newMyth.GetStats(id);
        return newMyth;
    }
    
    public void GetStats(int id)
    {
        string dir = Environment.CurrentDirectory;
        string csv = "/Assets/Resources/Units/UnitStats.csv";
        string path = Path.GetFullPath(dir + csv);
        StreamReader strReader = new StreamReader(path);

        this.Stats = new Dictionary<Stat, int>();

        string fileString = strReader.ReadToEnd();
        string[] lines = fileString.Split('\n');
        string[] fields = lines[id + 1].Split(';');
        if (fields[0].Equals(id.ToString()))
        {
            this.Stats.Add(Stat.hp, int.Parse(fields[2]));
            this.Stats.Add(Stat.armor, int.Parse(fields[3]));
            this.Stats.Add(Stat.barrier, int.Parse(fields[4]));
            this.Stats.Add(Stat.attack, int.Parse(fields[5]));
            this.Stats.Add(Stat.range, int.Parse(fields[6]));
            this.Stats.Add(Stat.attackType, int.Parse(fields[7]));
            
        }

    }

    #endregion
}

