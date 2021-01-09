using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Entity
{
    #region Attributes
    private int id;
    private string name;
    private Dictionary<Stat, int> stats;
    
    #endregion

    #region Getters & Setters
    public Dictionary<Stat, int> Stats { get => stats; set => stats = value; }
    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    #endregion

    #region Constructor
    public Entity()
    {
        stats = new Dictionary<Stat, int>();
    }

    public Entity(int id, string name)
    {
        stats = new Dictionary<Stat, int>();
        this.id = id;
        this.name = name;
        GameManager.gm.entities.Add(id,this);
    }

    public Entity(int id, int unitId)
    {
        stats = new Dictionary<Stat, int>();
        this.id = id;
        //TODO Parsing units from id
        GameManager.gm.entities.Add(id, this);
    }
    #endregion
}

