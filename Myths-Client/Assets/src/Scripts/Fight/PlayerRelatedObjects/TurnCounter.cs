using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCounter : MonoBehaviour
{
    public UnityEngine.UI.Text counter;
    public int turnCounter = 0;

    public void UpdateCounter()
    {
        counter.text = (turnCounter/2).ToString();
    }
}
