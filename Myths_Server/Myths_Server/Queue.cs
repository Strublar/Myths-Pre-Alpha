using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Myths_Server
{
    /**
     * Queue
     * Manages the matchmaking system
     * Stores the users in queue and start matchs
     */
    public class Queue
    {
        #region Attributes

        private List<User> users;
        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public Queue()
        {
            users = new List<User>();
        }
        #endregion

        #region Methods
        /**
        * AddUser
        * Add a new user to the queue
        * @param user : User to add to the queue
        */
        public void AddUser(User user)
        {
            users.Add(user);
            Console.WriteLine("User " +user.Id+" added to main queue, there are "+
                users.Count+" players in the queue");
            if (users.Count == 2)
            {
                //Thread.Sleep(100);
                StartMatch();
            }
        }

        /**
        * RemoveUser
        * Remove the user to the queue
        * @param user : User to remove from queue
        */
        public void RemoveUser(User user)
        {
            users.Remove(user);
            Console.WriteLine("User " +user.Id+" removed from main queue");
        }

        /**
        * StartMatch
        * Start a new match with the users in queue
        */
        public void StartMatch()
        {
            Console.WriteLine("New match created");
            List<User> newUserList = new List<User>
            {
                users[0],
                users[1]
            };
            MythsServer.Games.Add(new Game(newUserList));
            
            users.Clear();
        }

        #endregion
    }
}
