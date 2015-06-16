using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Tools;
using gbd.Tools.Cli;

namespace gbd.Dominion.Model.Zones
{
    public class Library : AbstractZone, ILibrary
    {

        public IDeck ParentDeck { get; private set; }



        public Library(IList<ICard> cards) : base(cards) { }

        public void Init(IDeck deck)
        {
            ParentDeck = deck;

            foreach (var card in ParentDeck.DiscardPile.Cards)
            {
                Cards.Add(card);
            }
            ParentDeck.DiscardPile.Cards.Clear();
            Cards.Shuffle();

        }

        public void ShuffleDiscardToLibrary()
        {
            this.ParentDeck.ShuffleDiscardToLibrary();
        }


        public void Add(ICard card, Position position)
        {
            switch (position)
            {
                case Position.Bottom:
                    Cards.Insert(0, card);
                    break;
                case Position.Top:
                    Cards.Add(card);
                    break;

                default:
                    throw new NotImplementedException();
            }
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


        public override int TotalCardsAvailable
        {
            get { return Cards.Count + ParentDeck.DiscardPile.Cards.Count; }
        }

    }
}