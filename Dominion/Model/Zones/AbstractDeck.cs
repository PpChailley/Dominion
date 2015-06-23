using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Tools;
using gbd.Tools.Cli;
using Ninject;

namespace gbd.Dominion.Model.Zones
{
    public abstract class AbstractDeck: IDeck
    {
        protected AbstractDeck(IDiscardPile discard, ILibrary lib, IBattleField bf, IHand hand)
        {
            DiscardPile = discard;
            Library = lib;
            BattleField = bf;
            Hand = hand;
        }

        [Inject]
        protected AbstractDeck(ILibrary lib)
        {
            DiscardPile = new DiscardPile();
            Library = lib;
            BattleField = new BattleField();
            Hand = new Hand();
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

                return toreturn.AsReadOnly();
            }

            set { throw new NotImplementedException(); }
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

        [Obsolete] 
        public void Add(ICard card, CardsPile destination)
        {
            Add(card, destination, Position.Top);
        }

        [Obsolete]
        public void Add(ICard card, CardsPile destination, Position position)
        {
            switch (destination)
            {
                case CardsPile.Discard:
                    DiscardPile.Cards.Add(card);
                    break;

                case CardsPile.Hand:
                    Hand.Add(card);
                    break;

                case CardsPile.Library:
                    Library.Add(card, position);
                    break;

                default:
                    throw new InvalidOperationException();

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

        public void Ready()
        {
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
