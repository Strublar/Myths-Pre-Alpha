using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * EndTurn
 * Script attached to the end turn button
 */
public class EndTurnButtonBehaviour : MonoBehaviour
{

    #region Control Methods
    public void OnClick()
    {

        Debug.Log("Fin de tour");
        Server.SendMessageToServer(new EndTurnMessage());
    }
    #endregion


}
