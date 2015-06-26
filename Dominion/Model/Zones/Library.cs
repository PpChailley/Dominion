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
    public class Library : AbstractZone, ILibrary
    {

        public IDeck ParentDeck { get; private set; }


        [Inject]
        public Library(IList<ICard> cards) : base(cards) { }

        public void Ready(IDeck deck)
        {
            ParentDeck = deck;
            Cards.ToList().ForEach(c => c.Ready(this));

            IoC.Kernel.Get<ICardShuffler>().Shuffle(this);
        }


        public IEnumerable<ICard> GetFromTop(int amount = 1)
        {
            var toreturn = new List<ICard>();

            for (var i = 0; i < amount; i++)
            {
                
                toreturn.Add(GetOneFromTop());
            }

            return toreturn;
        }

        private ICard GetOneFromTop()
        {
            if (Cards.Any() == false)
            {
                if (ParentDeck.Cards.Any() == false)
                {
                    throw new DeckEmptyException();
                }
                else
                {
                    ParentDeck.ShuffleDiscardToLibrary();
                }
            }

            var card = Cards.First();
            Cards.Remove(card);
            return card;
        }


        public new IList<ICard> Cards
        {
            get { return base.Cards; }
            set { base.Cards = value; }
        }

        public override int TotalCardsAvailable
        {
            get { return Cards.Count + ParentDeck.DiscardPile.Cards.Count; }
        }

        public new IEnumerable<ICard> Get(int amount, Position position = Position.Top)
        {
            if (amount > Cards.Count)
            {
                if (amount > TotalCardsAvailable)
                {
                    throw new NotEnoughCardsException(
                        "Cannot get {0} cards from {1}: {2} in library and {3} in discard"
                        .Format(amount, GetType().Name, Cards.Count, TotalCardsAvailable));
                }

                var beforeShuffle = Cards;
                Cards.Clear();
                ParentDeck.ShuffleDiscardToLibrary();
                beforeShuffle.Reverse().ToList().ForEach(c => this.PutCard(c, position));

            }

                return base.Get(amount, position);
        }




    }
}