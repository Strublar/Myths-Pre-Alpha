using Myths_Library;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 * CallCounter
 * Script attached to the call counter ui element
 */
public class ManaCounter : MonoBehaviour
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
        UpdateCounter();
    }

    public void UpdateCounter()
    {

        counterText.GetComponent<TextMeshProUGUI>().text = linkedPlayer.Stats[Stat.mana].ToString();
    }
    #endregion

}
