using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using org.gbd.Dominion.Model.Actions;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.Model
{
    public class Library :  ILibrary
    {

        private Queue<ICard> _cards;
        private readonly Deck _parentDeck;

        
        public Library(Deck deck)
        {
            _parentDeck = deck;
            _cards = new Queue<ICard>();

            Init();
        }

        private void Init()
        {
            Deck deck;
            foreach (var card in _parentDeck.Cards)
            {
                _cards.Enqueue(card);
            }
        }


        public IEnumerable<ICard> Cards
        {
            get { return _cards; }
        }

        public IEnumerable<ICard> Dequeue(int amount = 1)
        {
            var toreturn = new List<ICard>();

            for (var i = 0; i < amount; i++)
            {
                if (_cards.Any() == false)
                {
                    if (_parentDeck.Cards.Any() == false)
                    {
                        throw new DeckEmptyException();
                    }
                    else
                    {
                        _cards = _parentDeck.ShuffleToLibrary();
                    }
                }
                var card = _cards.Dequeue();
                toreturn.Add(card);
            }

            return toreturn;
        }



    }
}