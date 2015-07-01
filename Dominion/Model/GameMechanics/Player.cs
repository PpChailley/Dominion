using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Injection;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;
using Ninject;

namespace gbd.Dominion.Model.GameMechanics
{
    public class Player : IPlayer
    {

        public const int STARTING_HAND_SIZE = 5;

        public IIntelligence I { get; private set; }

        public int AvailableActions { get; set; }

        public int AvailableBuys { get; set; }

        public Resources AvailableResources{ get; set; }


        public PlayerStatus Status { get; set; }
        public IDeck Deck { get; set; }


        [Inject]
        public Player(IDeck deck, IIntelligence intel, PlayerStatus status)
        {
            AvailableActions = 0;
            AvailableResources = new Resources(0);
            AvailableBuys = 0;

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
            AvailableActions = 1;
            AvailableBuys = 1;
            AvailableResources = Resources.Zero;
        }

        public void EndTurn()
        {
            Deck.EndOfTurnCleanup();

            AvailableActions = 0;
            AvailableBuys = 0;
            AvailableResources = Resources.Zero;
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
            this.Status.Resources.Pay(card.Mechanics.Cost);
            Receive(card, ZoneChoice.Discard);
        }

        public void Play(ICard card)
        {
            if (AvailableActions < 1)
                throw new NotEnoughActionsException();

            AvailableActions --;
            card.MoveTo(Deck.BattleField);

            foreach (var trigger in card.Mechanics.OnPlayTriggers)
            {
                trigger.Do();
            }

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