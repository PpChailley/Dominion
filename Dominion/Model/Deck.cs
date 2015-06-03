using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using org.gbd.Dominion.Model.Cards;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.Model
{
    public class Deck: IDeck
    {

        
        private readonly IHand _hand = IoC.Kernel.Get<IHand>();
        private readonly IBattleField _playZone = IoC.Kernel.Get<IBattleField>();
        private readonly IDiscardPile _discard = IoC.Kernel.Get<IDiscardPile>();
        private readonly ILibrary _library = IoC.Kernel.Get<ILibrary>();
        


        


        public IHand Hand{ get { return _hand; } }
        public IDiscardPile DiscardPile{ get { return _discard; } }
        public ILibrary Library{ get { return _library; } }
        public IBattleField PlayZone { get { return _playZone; } }

        public IList<ICard> Cards
        {
            get
            {
                var toreturn = new List<ICard>();
                toreturn.AddRange(Library.Cards);
                toreturn.AddRange(Hand.Cards);
                toreturn.AddRange(DiscardPile.Cards);
                toreturn.AddRange(PlayZone.Cards);

                return toreturn.AsReadOnly();
            }

        }


        public int CurrentScore
        {
            get { return Cards.Sum(card => card.Mechanics.VictoryPoints); }
        }

        public void Add(ICard card, CardsPile destination)
        {
            Add(card, destination, PositionInCardsCollection.Top);
        }

        public void Add(ICard card, CardsPile destination, PositionInCardsCollection positionInCardsCollection)
        {
            switch (destination)
            {
                case CardsPile.Discard:
                    _discard.Cards.Add(card);
                    break;

                case CardsPile.Hand:
                    _hand.Add(card);
                    break;

                case CardsPile.Library:
                    _library.Add(card, positionInCardsCollection);
                    break;

                default:
                    throw new InvalidOperationException();

            }
        }

        public void EndOfTurnCleanup()
        {
            foreach (var card in PlayZone.Cards)
            {
                if (card.Attributes.Contains(CardAttribute.StayInPlayOnce))
                {
                    card.Attributes.Remove(CardAttribute.StayInPlayOnce);
                    continue;
                }

                DiscardPile.Cards.Add(card);
                PlayZone.Cards.Remove(card);
            }

            PlayZone.Cards.Clear();
        }

        public ILibrary ShuffleDiscardToLibrary()
        {
            foreach (var card in DiscardPile.Cards)
            {
                Library.Cards.Add(card);
            }
            Library.Cards.Shuffle();
            DiscardPile.Cards.Clear();

            return Library;
        }

        public void GetReadyToStartGame()
        {
            Library.Init(this);
        }

    }
}
