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
        public IHand Hand = IoC.Kernel.Get<IHand>();
        public IDiscardPile DiscardPile = IoC.Kernel.Get<IDiscardPile>();
        public ILibrary Library; 



        public Player()
        {
            Library = Deck.ShuffleToLibrary();
        }

        public int CurrentScore
        {
            get { return Deck.Cards.Sum(card => card.Mechanics.VictoryPoints); }
        }




        public void Draw(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Draw();
            }
        }

        public void Draw()
        {
            if (Library.Cards.Any() == false)
            {
                Shuffle();
            }

            Hand.Add(Library.Dequeue());


    foreach (var card in Library.Dequeue(amount))
            {
                Hand.Add(card);
            }
        }


        public void Discard(int amount)
        {
            throw new NotImplementedException();
        }

        public void Gain(ICard card)
        {
            Deck.Cards.Add(card);
            DiscardPile.Cards.Add(card);
        }
    }
}