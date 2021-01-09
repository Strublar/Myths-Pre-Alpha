using System;
using System.Collections.Generic;
using System.Text;


public enum Stat
{
    //Pure stats

    hp,
    armor,
    barrier,
    attack,
    energy,
    mobility,
    range,
    attackType,
    //Position stats
    x,
    y,

    //Matery
    mastery1,
    mastery2,
    mastery3,

    //Control stats
    canAttack,
    canMove,
    canRecall,
    canUlt,

    //State stats
    isDead,
    isCalled,
    isEngaged,

    //Player Stats
    calls,
    gaugeArcane,
    gaugeFire,
    gaugeWater,
    gaugeEarth,
    gaugeAir,
    gaugeLight,
    gaugeDark
}

public enum Mastery : int
{
    none = 0,
    arcane = 1,
    fire = 2,
    water = 3,
    earth = 4,
    air = 5,
    light = 6,
    dark = 7
}

