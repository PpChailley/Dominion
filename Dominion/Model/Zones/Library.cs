using System;
using System.Collections.Generic;
using System.Linq;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.Model
{
    public class Library : AbstractZone, ILibrary
    {

        private IDeck _parentDeck;

        

        public void Init(IDeck deck)
        {
            _parentDeck = deck;
            _cards = new List<ICard>();

            foreach (var card in _parentDeck.DiscardPile.Cards)
            {
                Cards.Add(card);
            }

            _parentDeck.DiscardPile.Cards.Clear();
            Cards.Shuffle();
        }

        public void ShuffleDiscardToLibrary()
        {
            this._parentDeck.ShuffleDiscardToLibrary();
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
            if (_cards.Any() == false)
            {
                if (_parentDeck.Cards.Any() == false)
                {
                    throw new DeckEmptyException();
                }
                else
                {
                    _parentDeck.ShuffleDiscardToLibrary();
                }
            }

            var card = _cards.First();
            _cards.Remove(card);
            return card;
        }


        public override int TotalCardsAvailable
        {
            get { return Cards.Count + _parentDeck.DiscardPile.Cards.Count; }
        }

    }
}