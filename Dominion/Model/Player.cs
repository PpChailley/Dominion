using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace org.gbd.Dominion.Model.Actions
{
    public class Player
    {

        #region Static Accessors

        public static ICollection<Player> Get(ICollection<PlayerChoice> designatedPlayers)
        {
            return designatedPlayers.Select(player => Get(player)).ToList();
        }

        private static Player Get(PlayerChoice designatedPlayer)
        {
            switch (designatedPlayer)
            {
                case PlayerChoice.Current:
                    return CurrentPlayer;

                case PlayerChoice.Left:
                case PlayerChoice.Right:
                    throw new NotImplementedException();

                default :
                    throw new NotImplementedException();


            }
        }

        public static Player CurrentPlayer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion


        public String Name;

        public int CurrentScore
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IDeck Deck;
        public ILibrary Library;
        public IHand Hand;


        public void Draw(int amount)
        {
            foreach (var card in Library.Dequeue(amount))
            {
                Hand.Add(card);
            }
        }


        public void Discard(int amount)
        {
            throw new NotImplementedException();
        }
    }
}