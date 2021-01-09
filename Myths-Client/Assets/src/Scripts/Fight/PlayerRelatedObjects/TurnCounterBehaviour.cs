﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCounterBehaviour : MonoBehaviour
{
    public UnityEngine.UI.Text counter;
    public int turnCounter = 0;

    public void UpdateCounter()
    {
        counter.text = turnCounter.ToString();
    }
}
