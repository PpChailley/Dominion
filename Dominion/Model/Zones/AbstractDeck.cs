using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Tools;
using gbd.Tools.Clr;
using Ninject;

namespace gbd.Dominion.Model.Zones
{
    public abstract class AbstractDeck: IDeck
    {
  

        [Inject]
        protected AbstractDeck(ILibrary lib, IDiscardPile discard, IBattleField field, IHand hand)
        {
            DiscardPile = discard;
            Library = lib;
            BattleField = field;
            Hand = hand;
        }
        
        
        public IHand Hand { get; protected set; }
        public IDiscardPile DiscardPile { get; protected set; }
        public ILibrary Library { get; protected set; }

        public IBattleField BattleField { get; protected set; }

        public IList<ICard> Cards
        {
            get
            {
                var toreturn = new List<ICard>();
                toreturn.AddRange(Library.Cards);
                toreturn.AddRange(Hand.Cards);
                toreturn.AddRange(DiscardPile.Cards);
                toreturn.AddRange(BattleField.Cards);

                //TODO: test this line
                return toreturn.AsReadOnly();
            }

            set { throw new NotImplementedException(); }
        }

        public IEnumerable<ICard> Get(int amount, Position positionFrom = Position.Top)
        {
            throw new InvalidOperationException("Cannot Get cards from a deck. Try from Library instead.");
        }

        public void SortCards(Func<ICard, IComparable> comparer)
        {
            throw new InvalidOperationException("Cannot sort cards from a deck. Try sorting the library instead.");
        }

        public int TotalCardsAvailable
        {
            get { return Cards.Count; }
        }


        public int CurrentScore
        {
            get { return Cards.Sum(card => card.Mechanics.VictoryPoints); }
        }

        public CardRepartition CardCountByZone
        {
            get
            {
                return new CardRepartition(Library.Cards.Count, Hand.Cards.Count, DiscardPile.Cards.Count, BattleField.Cards.Count);
            }
        }


        public void EndOfTurnCleanup()
        {
            foreach (var card in BattleField.Cards)
            {
                if (card.Attributes.Contains(CardAttribute.StayInPlayOnce))
                {
                    card.Attributes.Remove(CardAttribute.StayInPlayOnce);
                    continue;
                }

                //TODO: remove that mess
                DiscardPile.Cards.Add(card);
                BattleField.Cards.Remove(card);
            }

            BattleField.Cards.Clear();
        }

        public ILibrary ShuffleDiscardToLibrary()
        {
            foreach (var card in DiscardPile.Cards)
            {
                Library.Cards.Add(card);
            }

            IoC.Kernel.Get<ICardShuffler>().Shuffle(Library);
            DiscardPile.Cards.Clear();

            return Library;
        }

        public IZone Get(ZoneChoice zone)
        {
            switch (zone)
            {
                case ZoneChoice.Discard:
                    return DiscardPile;

                case ZoneChoice.Library:
                    return Library;

                case ZoneChoice.Hand:
                    return Hand;

                case ZoneChoice.Play:
                    return BattleField;

                default:
                    throw new InvalidOperationException();
            }
        }


        public void Ready()
        {
            DiscardPile.Ready();
            Hand.Ready();
            BattleField.Ready();

            // TODO: test that
            DiscardPile.MoveCardsTo(Library, DiscardPile.Cards.Count);
            Hand.MoveCardsTo(Library, Hand.Cards.Count);
            BattleField.MoveCardsTo(Library, BattleField.Cards.Count);

            Library.Ready(this);
        }

        public override string ToString()
        {
            return String.Format("{0} # {1} with {2}",   
                    this.GetType().Name,
                    this.GetHashCode(),
                    this.CardCountByZone);
        }
    }
}
