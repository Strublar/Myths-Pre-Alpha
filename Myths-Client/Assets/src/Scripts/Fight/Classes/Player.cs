using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Player : Entity
{
    #region Attributes
    private int calls;
    private int playerId;
    private List<Myth> team;
    #endregion

    #region Getters & Setters
    public int Calls { get => calls; set => calls = value; }
    public int PlayerId { get => playerId; set => playerId = value; }
    public List<Myth> Team { get => team; set => team = value; }
    #endregion

    #region Constructor
    public Player (int playerId, int entityId, string name) : base(entityId,name)
    {
        this.PlayerId = playerId;
        Team = new List<Myth>
        {
            new Myth(),
            new Myth(),
            new Myth(),
            new Myth(),
            new Myth()
        };
        this.Stats.Add(Stat.calls, 0);

        //Gauges
        this.Stats.Add(Stat.gaugeArcane, 0);
        this.Stats.Add(Stat.gaugeLight, 0);
        this.Stats.Add(Stat.gaugeDark, 0);
        this.Stats.Add(Stat.gaugeFire, 0);
        this.Stats.Add(Stat.gaugeEarth, 0);
        this.Stats.Add(Stat.gaugeAir, 0);
        this.Stats.Add(Stat.gaugeWater, 0);
    }
    #endregion
}

