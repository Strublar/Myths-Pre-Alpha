using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "NewListeningEffect", menuName = "ScriptableObjects/ListeningEffect", order = 1)]
public class ListeningEffect : ScriptableObject
{
    [Header("Infos")]
    public int id;
    public string passiveName;
    public Sprite icon;
    public string description;

    [Header("Triggers")]
    public Trigger[] executionTriggers;
    public Trigger[] endTriggers;
    [Header("Effect")]
    public EffectsGroup[] effects;
}



