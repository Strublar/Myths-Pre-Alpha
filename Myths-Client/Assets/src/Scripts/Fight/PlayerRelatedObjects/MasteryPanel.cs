using Myths_Library;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MasteryPanel : MonoBehaviour
{
    #region Variables
    public Player linkedPlayer;
    public TextMeshProUGUI lightText, fireText, waterText, airText, earthText, darkText;

    #endregion

    public void Init(Player player)
    {
        this.linkedPlayer = player;
        UpdateMasteries();
    }

    public void UpdateMasteries()
    {
        lightText.text = linkedPlayer.Stats[Stat.masteryLight].ToString();
        fireText.text = linkedPlayer.Stats[Stat.masteryFire].ToString();
        waterText.text = linkedPlayer.Stats[Stat.masteryWater].ToString();
        airText.text = linkedPlayer.Stats[Stat.masteryAir].ToString();
        earthText.text = linkedPlayer.Stats[Stat.masteryEarth].ToString();
        darkText.text = linkedPlayer.Stats[Stat.masteryDark].ToString();
    }
}
