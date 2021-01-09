using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Defines
{
    public static int tileScale = 10;
}
public enum DamageType
{
    physical,
    magical
}

public enum StatusType
{
    passive,
    callEffect
}

public enum GameState
{
    beginning,
    turn,
    placeLead,
    call,
    castSpell,
    move,
    attack

}

public enum SideSelector
{
    ally,
    enemy,
    all
}