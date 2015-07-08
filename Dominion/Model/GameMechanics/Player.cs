using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Injection;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;
using Ninject;
using NLog;

namespace gbd.Dominion.Model.GameMechanics
{
    public class Player : IPlayer
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();


        public const int STARTING_HAND_SIZE = 5;

        public IIntelligence I { get; private set; }

        public PlayerStatus Status { get; set; }
        public IDeck Deck { get; set; }


        [Inject]
        public Player(IDeck deck, IIntelligence intel, PlayerStatus status)
        {
            Deck = deck;
            I = intel;
            Status = status;
        }



        public void Ready()
        {
            Deck.Ready();
            I.Ready(this);
            Draw(STARTING_HAND_SIZE);
        }

        public void StartTurn()
        {
            Log.Info("Start of turn for {0}", this);

            Status.StartTurn();
        }

        public void EndTurn()
        {
            Log.Info("End of turn for {0}", this);

            Deck.EndOfTurnCleanup();

            Status.EndTurn();
        }



        public void Draw(int amount = 1)
        {
            Deck.Library.MoveCardsTo(Deck.Hand, amount);
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
            if (Status.Buys < 1)
                throw new NotEnoughBuysException();

            Status.Resources.Pay(card.Mechanics.Cost);
            Status.Buys--;
            Receive(card, ZoneChoice.Discard);
        }

        public void PlayAction(ICard card)
        {
            if (this != IoC.Kernel.Get<IGame>().CurrentPlayer)
                throw new InvalidOperationException("Player is not active and cannot play an action");

            if (Status.Actions < 1)
                throw new NotEnoughActionsException();

            if (card.Mechanics.GetCardType<ActionType>() == null)
                throw new InvalidOperationException("Card is not an action and cannot be played as one");

            Status.Actions--;
            card.MoveTo(Deck.BattleField);

            foreach (var trigger in card.Mechanics.OnPlayTriggers)
            {
                trigger.Do();
            }

        }

        public void PlayTreasure(ICard card)
        {
            if (this != IoC.Kernel.Get<IGame>().CurrentPlayer)
                throw new InvalidOperationException("Player is not active and cannot play a treasure");

            if (card.Mechanics.GetCardType<TreasureType>() == null)
                throw new InvalidOperationException("Card is not a treasure and cannot be played as one");

            card.MoveTo(Deck.BattleField);


            foreach (var trigger in card.Mechanics.OnPlayTriggers)
            {
                trigger.Do();
            }

            // TODO: with some special treasure cards this will report a wrong count
            // Probably need to count extra resources + resources in play - spent
            Status.Resources = Status.Resources.Plus(card.Mechanics.TreasureValue);
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




        public override string ToString()
        {
            return String.Format("{0} # {1} with {{ {2} {3} }}",
                this.GetType().Name,
                this.GetHashCode(),
                this.Deck.GetType().Name,
                this.Deck.CardCountByZone);
        }

    }
}