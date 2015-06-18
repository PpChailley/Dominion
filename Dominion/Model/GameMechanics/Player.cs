using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using Ninject;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Tools;

namespace gbd.Dominion.Model.GameMechanics
{
    public class Player : IPlayer
    {

        public const int STARTING_HAND_SIZE = 5;



        #region Static Accessors

        public static ICollection<IPlayer> Get(ICollection<PlayerChoice> designatedPlayers)
        {
            return designatedPlayers.Select(player => Get((PlayerChoice) player)).ToList();
        }

        private static IPlayer Get(PlayerChoice designatedPlayer)
        {
            switch (designatedPlayer)
            {
                case PlayerChoice.Current:
                    return Game.G.CurrentPlayer;

                case PlayerChoice.Left:
                case PlayerChoice.Right:
                    throw new NotImplementedException();

                default :
                    throw new NotImplementedException();


            }
        }

        #endregion


        private readonly IIntelligence _intelligence;

        public PlayerStatus Status { get; set; }
        public IDeck Deck { get; set; }
        public String Name;


        public Player(IDeck deck, IIntelligence intel, PlayerStatus status)
        {
            Deck = deck;
            _intelligence = intel;
            Status = status;
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


        public void AddToDeck(ICard card, CardsPile destination = CardsPile.Discard, Position position = Position.Top)
        {
            Deck.Add(card, destination, position);
        }

        public void Receive(IList<ICard> cards)
        {
            Game.MoveCards(cards, Game.G.SupplyZone, DiscardPile, Position.Top);
        }
        public void Receive(ICard card)
        {
            Receive(new List<ICard>() { card });
        }

        public void Buy(IList<ICard> cards)
        {
            this.Status.Resources.Pay(cards.Select(card => card.Mechanics.Cost));
            Receive(cards);
        }
        public void Buy(ICard card)
        {
            Buy(new List<ICard>() { card });
        }

    }
}