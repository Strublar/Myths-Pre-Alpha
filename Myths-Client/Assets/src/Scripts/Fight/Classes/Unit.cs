using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Unit : Entity
{
    #region Attributes

    private int ownerId;
    private GameObject model;
    private Passive[] passives;
    #endregion

    #region Getters & Setters
    public GameObject Model { get => model; set => model = value; }
    public int OwnerId { get => ownerId; set => ownerId = value; }
    public Passive[] Passives { get => passives; set => passives = value; }

    #endregion

    #region Constructor
    public Unit()
    {

        
    }

    public Unit(int ownerId,int entityId,int unitId) : base(entityId, unitId)
    {
        this.ownerId = ownerId;
        this.Stats.Add(Stat.hp, 0);
        this.Stats.Add(Stat.armor, 0);
        this.Stats.Add(Stat.barrier, 0);
        this.Stats.Add(Stat.attack, 0);
        this.Stats.Add(Stat.mobility, 2);
        this.Stats.Add(Stat.range, 1);
        this.Stats.Add(Stat.attackType, 1);
        this.Stats.Add(Stat.canAttack, 0);
        this.Stats.Add(Stat.canMove, 0);
        this.Stats.Add(Stat.isCalled, 0);
        this.Stats.Add(Stat.isDead, 0);
        this.Stats.Add(Stat.x, 0);
        this.Stats.Add(Stat.y, 0);
        this.Stats.Add(Stat.mastery1, 0);
        this.Stats.Add(Stat.mastery2, 0);
        this.Stats.Add(Stat.mastery3, 0);
        ParseUnit(unitId);
    }
    #endregion

    #region Parsing methods
    public void ParseUnit(int unitId)
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

            this.Name = fields[1].TrimEnd(new char[] {'\r','\n'});
            this.passives = new Passive[2];
            this.passives[0] = Passive.ParsePassive(int.Parse(fields[9]));
            this.passives[1] = Passive.ParsePassive(int.Parse(fields[10]));

        }
        else
        {
            throw new Exception(" Invalid Unit Id given in builder, table might be wrong");
        }
    }
    #endregion
}

