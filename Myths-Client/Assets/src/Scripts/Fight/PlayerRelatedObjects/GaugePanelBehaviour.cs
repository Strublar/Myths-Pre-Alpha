using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GaugePanelBehaviour : MonoBehaviour
{
    public Player linkedPlayer;
    public TextMeshProUGUI[] gaugeTags;


    public void InitGauge(Player player)
    {
        this.linkedPlayer = player;
    }

    public void UpdateGauge()
    {
        gaugeTags[0].text = linkedPlayer.Stats[Stat.gaugeArcane].ToString();
        gaugeTags[1].text = linkedPlayer.Stats[Stat.gaugeLight].ToString();
        gaugeTags[2].text = linkedPlayer.Stats[Stat.gaugeDark].ToString();
        gaugeTags[3].text = linkedPlayer.Stats[Stat.gaugeFire].ToString();
        gaugeTags[4].text = linkedPlayer.Stats[Stat.gaugeEarth].ToString();
        gaugeTags[5].text = linkedPlayer.Stats[Stat.gaugeAir].ToString();
        gaugeTags[6].text = linkedPlayer.Stats[Stat.gaugeWater].ToString();
    }
}
