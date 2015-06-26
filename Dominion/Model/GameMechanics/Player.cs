using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Tools;
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
            Deck.Library.MoveCardsTo(Deck.Hand, amount);
        }

        public void ChooseAndDiscard(int amount)
        {
            var toDiscard = this._intelligence.ChooseAndDiscard(amount);
            toDiscard.MoveTo(Deck.DiscardPile);
        }


        public void Receive(IList<ICard> cards, ZoneChoice to, Position position = Position.Top)
        {
            cards.MoveTo(Deck.Get(to), position);
        }

        public void Receive(ICard card, ZoneChoice to = ZoneChoice.Discard, Position position = Position.Top)
        {
            card.MoveTo(Deck.Get(to), position);
        }

        public void Buy(ICard card)
        {
            this.Status.Resources.Pay(card.Mechanics.Cost);
            Receive(card, ZoneChoice.Discard);
        }

        public void Play(ICard card)
        {
            foreach (var trigger in card.Mechanics.OnPlayTriggers)
            {
                trigger.Do();
            }

            card.MoveTo(Deck.BattleField);
        }

        public void ReceiveFrom(ISupplyPile @from, int amount, ZoneChoice to, Position position = Position.Top)
        {
            try
            {
                for (int i = 0; i < amount; i++)
                {
                    var card = from.Get(1).Single();
                    Receive(card, to, position);
                }
            }
            catch (NotEnoughCardsException)
            {
                // TODO: at least there should be some logging here
            }
        }

        public void ChooseAndReceive(Resources maxCost)
        {
            throw new NotImplementedException();
        }

        public ICard[] ChooseAndTrash(ZoneChoice from, int numberOfCards)
        {
            throw new NotImplementedException();
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