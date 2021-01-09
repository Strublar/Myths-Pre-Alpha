using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * CallCounter
 * Script attached to the call counter ui element
 */
public class CallCounterBehaviour : MonoBehaviour
{
    #region Variables
    public Player linkedPlayer;
    public GameObject counterText;
    #endregion


    #region Unity Methods
    // Update is called once per frame


    public void InitCounter(Player player)
    {
        this.linkedPlayer = player;
    }

    public void UpdateCounter()
    {

        counterText.GetComponent<Text>().text = linkedPlayer.Stats[Stat.calls].ToString();
    }
    #endregion

}
