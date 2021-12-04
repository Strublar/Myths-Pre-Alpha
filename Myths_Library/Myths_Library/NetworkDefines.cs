using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    #region Messages sent by client

    public enum ClientMessageType : byte
    {

        //Auth messages
        Login,
        Logout,
        JoinQueue,
        LeaveQueue,
        //Fight messages
        Call,
        Recall,
        Attack,
        CastSpell,
        Move,
        EndTurn


    }


    #endregion

    #region Messages sent by server
    public enum ServerMessageType : byte
    {
        //Auth Messages
        LoggedIn,
        LoggedOut,
        QueueJoined,
        QueueLeft,
        MatchFound,

        //Fight Messages

        //animation triggering messages
        UnitAttack,
        UnitMoved,
        UnitCalled,
        UnitUncalled,
        SpellCast,
        BeginTurn,
        EndTurn,
        EffectProc,
        StatusChanged,
        CapturePortal,

        //Value update messages
        EntityStatChanged,

        //Start of the game messages
        InitPlayer,
        InitMyth,
        InitPortal,
        StartGame,
        EndGame
    }
    #endregion
}
