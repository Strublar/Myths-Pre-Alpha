using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName: MonoBehaviour
{
    #region attributes
    private string userName;

    #endregion

    #region Getters & Setters
    public string UserName { get => userName; set => userName = value; }

    #endregion

    #region Display methods
    public void UpdateName(string name)
    {
        userName = name;
        this.GetComponentInChildren<TextMeshProUGUI>().text = userName;
    }

    public void UpdateCurrentPlayer(bool isCurrentPlayer)
    {
        if(isCurrentPlayer)
        {
            this.GetComponentInChildren<Image>().color = new Color32(255, 255, 150, 255);
        }
        else
        {
            this.GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
        }
    }
    #endregion
}
