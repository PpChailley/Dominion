using System;
using System.Collections.Generic;
using System.Linq;
using NLog.Fluent;
using NUnit.Framework;
using org.gbd.Dominion.Model.Actions;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.Model
{
    public class Library :  ILibrary
    {

        private IList<ICard> _cards = new List<ICard>();
        private IDeck _parentDeck;

        

        public void Init(IDeck deck)
        {
            _parentDeck = deck;
            _cards = new List<ICard>();

            foreach (var card in _parentDeck.DiscardPile.Cards)
            {
                _cards.Add(card);
            }

            _parentDeck.DiscardPile.Cards.Clear();
            _cards.Shuffle();
        }


        public IList<ICard> Cards
        {
            get { return _cards; }
        }



        public void Add(ICard card, PositionInCardsCollection position)
        {
            switch (position)
            {
                case PositionInCardsCollection.Bottom:
                    Cards.Insert(0, card);
                    break;
                case PositionInCardsCollection.Top:
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
    }
}