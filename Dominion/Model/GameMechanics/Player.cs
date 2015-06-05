using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.Model.GameMechanics
{
    public interface IPlayer
    {
        IHand Hand { get; }
        IDiscardPile DiscardPile { get; }
        ILibrary Library { get; }
        int CurrentScore { get; }
        void GetReadyToStartGame();
        void Draw(int amount = 1);
        void DiscardFromHand(int amount);
        void Gain(ICard card, CardsPile destination = CardsPile.Discard, Position position = Position.Top);
        void Receive(ICard card);
    }

    public class Player : IPlayer
    {

        public const int STARTING_HAND_SIZE = 5;



        #region Static Accessors

        public static ICollection<Player> Get(ICollection<PlayerChoice> designatedPlayers)
        {
            return designatedPlayers.Select(player => Get((PlayerChoice) player)).ToList();
        }

        private static Player Get(PlayerChoice designatedPlayer)
        {
            switch (designatedPlayer)
            {
                case PlayerChoice.Current:
                    return Game.CurrentPlayer;

                case PlayerChoice.Left:
                case PlayerChoice.Right:
                    throw new NotImplementedException();

                default :
                    throw new NotImplementedException();


            }
        }

        #endregion


        private readonly IIntelligence _intelligence;


        public String Name;

        public IDeck Deck;

        public Player()
        {
            Deck = IoC.Kernel.Get<IDeck>();
            _intelligence = IoC.Kernel.Get<IIntelligence>();
        }

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
            _intelligence.Init(this);
            Draw(STARTING_HAND_SIZE);
        }


        public void Draw(int amount = 1)
        {
            Game.MoveCards(Library, Hand, amount);
        }

        public void DiscardFromHand(int amount)
        {
            var toDiscard = this._intelligence.ChooseAndDiscard(amount);
            Game.MoveCards(toDiscard, Hand, this.DiscardPile, Position.Top);
        }


        public void Gain(ICard card, CardsPile destination = CardsPile.Discard, Position position = Position.Top)
        {
            Deck.Add(card, destination, position);
        }

        public void Receive(ICard card)
        {
            Game.MoveCards(new List<ICard> {card}, Game.SupplyZone, DiscardPile, Position.Top);
        }
    }
}