using Myths_Library;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightProcessor : MonoBehaviour
{

    public static Queue<Action<Message>> fightUpdates;
    public static Queue<Message> fightUpdatesParam;

    private void Update()
    {

        while (fightUpdates.Count > 0)
        {
            fightUpdates.Dequeue().Invoke(fightUpdatesParam.Dequeue());
        }


    }

    #region Message processing methods

    public static void OnEntityStatCHanged(Message message)
    {
        EntityStatChangedMessage mess = message as EntityStatChangedMessage;
        Debug.Log("EntityStatchanged recieved");
        GameManager.gm.EntityStatChanged(GameManager.gm.entities[mess.targetId], mess.stat, mess.newValue);

    }

    public static void OnUnitCalled(Message message)
    {
        EntityCalledMessage mess = message as EntityCalledMessage;
        GameManager.gm.Call((Unit)GameManager.gm.entities[mess.targetId], mess.x, mess.y);

    }

    public static void OnUnitMoved(Message message)
    {
        EntityMovedMessage mess = message as EntityMovedMessage;

        GameManager.gm.MoveUnit((Unit)GameManager.gm.entities[mess.targetId], mess.x, mess.y);
        
    }



    public static void OnUnitUncalled(Message message)
    {
        EntityUnCalledMessage mess = message as EntityUnCalledMessage;
        GameManager.gm.UnCall((Unit)GameManager.gm.entities[mess.targetId]);
        
    }

    public static void OnBeginTurn(Message message)
    {
        BeginTurnMessage mess = message as BeginTurnMessage;
        GameManager.gm.BeginTurn(mess.team);
        
    }

    public static void OnEndGame(object[] parameters)
    {
        if (parameters[0] is int winnerId)
        {
            GameManager.gm.EndGame(winnerId);
        }
    }

    public static void OnSpellCast(object[] parameters)
    {
        if (parameters[0] is int casterId &&
            parameters[1] is int spellId &&
            parameters[2] is int x &&
            parameters[3] is int y)
        {
            GameManager.gm.SpellCast(casterId, spellId, x, y);
        }
    }


    #endregion
}
