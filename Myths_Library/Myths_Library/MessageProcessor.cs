using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Library
{
    public class MessageProcessor
    {

        public Dictionary<byte, Type> clientMessageGenerator = new Dictionary<byte, Type>();
        public Dictionary<byte, Type> serverMessageGenerator = new Dictionary<byte, Type>();

        public Dictionary<Type, Action<Message>> processor = new Dictionary<Type, Action<Message>>();

        public MessageProcessor()
        {
            #region Client Messages
            clientMessageGenerator.Add((byte)ClientMessageType.Login, typeof(LoginMessage));
            clientMessageGenerator.Add((byte)ClientMessageType.Logout, typeof(LogoutMessage));
            clientMessageGenerator.Add((byte)ClientMessageType.JoinQueue, typeof(JoinQueueMessage));
            clientMessageGenerator.Add((byte)ClientMessageType.LeaveQueue, typeof(LeaveQueueMessage));
            /*clientMessageGenerator.Add((byte)ClientMessageType.Call, OnCall);
            clientMessageGenerator.Add((byte)ClientMessageType.Recall, OnRecall);
            clientMessageGenerator.Add((byte)ClientMessageType.Attack, OnAttack);
            clientMessageGenerator.Add((byte)ClientMessageType.CastSpell, OnCastSpell);
            clientMessageGenerator.Add((byte)ClientMessageType.Move, OnMove);
            clientMessageGenerator.Add((byte)ClientMessageType.EndTurn, OnEndTurn);*/
            #endregion

            #region Server Messages
            serverMessageGenerator.Add((byte)ServerMessageType.LoggedIn, typeof(LoggedInMessage));
            serverMessageGenerator.Add((byte)ServerMessageType.LoggedOut, typeof(LoggedOutMessage));
            serverMessageGenerator.Add((byte)ServerMessageType.QueueJoined, typeof(QueueJoinedMessage));
            serverMessageGenerator.Add((byte)ServerMessageType.QueueLeft, typeof(QueueLeftMessage));
            serverMessageGenerator.Add((byte)ServerMessageType.MatchFound, typeof(MatchFoundMessage));

            serverMessageGenerator.Add((byte)ServerMessageType.InitPlayer, typeof(InitPlayerMessage));
            serverMessageGenerator.Add((byte)ServerMessageType.InitMyth, typeof(InitMythMessage));

            //Fight messages
            //Start game messages
            /*serverMessageGenerator.Add((byte)ServerMessageType.InitPlayer, OnInitPlayer);
            serverMessageGenerator.Add((byte)ServerMessageType.InitMyth, OnInitMyth);
            serverMessageGenerator.Add((byte)ServerMessageType.StartGame, OnStartGame);

            //other fight messages
            serverMessageGenerator.Add((byte)ServerMessageType.EntityStatChanged, OnEntityStatChanged);
            serverMessageGenerator.Add((byte)ServerMessageType.UnitCalled, OnUnitCalled);
            serverMessageGenerator.Add((byte)ServerMessageType.UnitMoved, OnUnitMoved);
            serverMessageGenerator.Add((byte)ServerMessageType.UnitAttack, OnUnitAttack);
            serverMessageGenerator.Add((byte)ServerMessageType.UnitUncalled, OnUnitUncalled);
            serverMessageGenerator.Add((byte)ServerMessageType.BeginTurn, OnBeginTurn);
            serverMessageGenerator.Add((byte)ServerMessageType.EndGame, OnEndGame);
            serverMessageGenerator.Add((byte)ServerMessageType.SpellCast, OnSpellCast);
            serverMessageGenerator.Add((byte)ServerMessageType.InitPortal, OnInitPortal);
            serverMessageGenerator.Add((byte)ServerMessageType.CapturePortal, OnCapturePortal);*/

            #endregion
        }
        public void Process(Message message)
        {
            Console.WriteLine("Type : " + message.GetType().ToString());
            processor[message.GetType()].Invoke(message);
        }


        public Message GenerateClientMessage(byte[] buffer)
        {
            Message newMessage = (Message)Activator.CreateInstance(clientMessageGenerator[buffer[0]]);
            return newMessage;
        }
        public Message GenerateServerMessage(byte[] buffer)
        {
            Message newMessage = (Message)Activator.CreateInstance(serverMessageGenerator[buffer[0]]);
            return newMessage;
        }
    }
}
