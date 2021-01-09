using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameBehaviour : MonoBehaviour
{
    #region attributes
    private string playerName;

    #endregion

    #region Getters & Setters
    public string PlayerName { get => playerName; set => playerName = value; }

    #endregion

    #region Display methods
    public void UpdateName(string name)
    {
        playerName = name;
        this.GetComponentInChildren<TextMeshProUGUI>().text = playerName;
    }

    public void UpdateCurrentPlayer(bool isCurrentPlayer)
    {
        if(isCurrentPlayer)
        {
            this.GetComponent<Image>().color = new Color32(255, 255, 150, 255);
        }
        else
        {
            this.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }
    #endregion
}
