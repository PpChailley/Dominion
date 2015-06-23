using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Tools;
using gbd.Tools.Cli;
using Ninject;

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
                    return IoC.Kernel.Get<IGame>().CurrentPlayer;

                case PlayerChoice.Left:
                case PlayerChoice.Right:
                    throw new NotImplementedException();

                default :
                    throw new NotImplementedException();


            }
        }

        #endregion


        private readonly IIntelligence _intelligence;

        public int AvailableActions { get; set; }

        public int AvailableBuys { get; set; }

        public int AvailableCoins{ get; set; }


        public PlayerStatus Status { get; set; }
        public IDeck Deck { get; set; }
        public String Name;


        [Inject]
        public Player(IDeck deck, IIntelligence intel, PlayerStatus status)
        {
            AvailableActions = 0;
            AvailableCoins = 0;
            AvailableBuys = 0;

            Deck = deck;
            _intelligence = intel;
            Status = status;
        }



        public int CurrentScore
        {
            get { return Deck.CurrentScore; }
        }



        public void Ready()
        {
            Deck.Ready();
            _intelligence.Init(this);
            Draw(STARTING_HAND_SIZE);
        }

        public void StartTurn()
        {
            throw new NotImplementedException();
        }

        public void EndTurn()
        {
            throw new NotImplementedException();
        }


        public void Draw(int amount = 1)
        {
            Model.MoveCards(Deck.Library, Deck.Hand, amount);
        }

        public void DiscardFromHand(int amount)
        {
            var toDiscard = this._intelligence.ChooseAndDiscard(amount);
            Model.MoveCards(toDiscard, Deck.Hand, Deck.DiscardPile, Position.Top);
        }


        public void AddToDeck(ICard card, CardsPile destination = CardsPile.Discard, Position position = Position.Top)
        {
            Deck.Add(card, destination, position);
        }

        public void Receive(IList<ICard> cards)
        {
            Model.MoveCards(cards, IoC.Kernel.Get<IGame>().SupplyZone, Deck.DiscardPile, Position.Top);
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

        public void Play(ICard card)
        {
            foreach (var trigger in card.Mechanics.OnPlayTriggers)
            {
                trigger.Do();
            }

            Model.MoveCard(card, Deck.Hand, Deck.BattleField);
        }

        public void ReceiveFrom(ISupplyPile @from, int amount)
        {
            try
            {
                for (int i = 0; i < amount; i++)
                {
                    var card = from.Get(1).Single();
                    Receive(card);
                }
            }
            catch (NotEnoughCardsException) { }
        }

        public void ChooseAndReceive(Resources maxCost)
        {
            throw new NotImplementedException();
        }

        public ICard[] ChooseAndTrash(ZoneChoice @from, int numberOfCards)
        {
            throw new NotImplementedException();
        }


        public void Buy(ICard card)
        {
            Buy(new List<ICard>() { card });
        }


        public override string ToString()
        {
            return String.Format("{0} # {1} with {2} {3}",
                this.GetType().Name,
                this.GetHashCode(),
                this.Deck.GetType(),
                this.Deck.CardCountByZone);
        }
    }
}