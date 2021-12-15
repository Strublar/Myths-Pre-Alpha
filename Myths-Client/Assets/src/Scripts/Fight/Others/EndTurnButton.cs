using Myths_Library;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * EndTurn
 * Script attached to the end turn button
 */
public class EndTurnButton : MonoBehaviour
{

    #region Control Methods
    public void OnClick()
    {
        Server.SendMessageToServer(new EndTurnMessage());
    }
    #endregion


}
