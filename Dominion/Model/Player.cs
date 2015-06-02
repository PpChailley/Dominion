using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.Model
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

        public IDeck Deck = IoC.Kernel.Get<IDeck>();
        public IHand Hand{ get { return Deck.Hand; }}
        public IDiscardPile DiscardPile{ get { return Deck.DiscardPile; }}
        public ILibrary Library{ get { return Deck.Library; }} 


        public int CurrentScore
        {
            get { return Deck.CurrentScore; }
        }

        public void GetReadyToStartGame()
        {
            Deck.GetReadyToStartGame();
        }



        public void Draw(int amount = 1)
        {
            Hand.Add(Library.GetFromTop(amount).ToList());
        }

        public void Discard(int amount)
        {
            throw new NotImplementedException();
        }

        public void Gain(ICard card, CardsPile destination = CardsPile.Discard, PositionInCardsCollection position = PositionInCardsCollection.Top)
        {
            Deck.Add(card, destination, position);
        }
    }
}