using System;
using System.Collections.Generic;
using System.Linq;
using org.gbd.Dominion.Model.GameMechanics;

namespace org.gbd.Dominion.Model.Zones
{
    public abstract class AbstractZone: IZone
    {

        protected IList<ICard> _cards = new List<ICard>();

        public IList<ICard> Cards
        {
            get { return _cards; }
        }

        public IEnumerable<ICard> Get(int amount, Position positionFrom)
        {
            var cardsCount = Cards.Count;

            if (Cards.Count < amount)
            {
                throw new NotEnoughCardsException();
            }

            var toreturn = new List<ICard>();

            for (var i = 0; i < amount; i++)
            {
                switch (positionFrom)
                {
                    case Position.Top:
                        toreturn.Add(Cards.ElementAt(i));
                        break;

                    case Position.Bottom:
                        int index = cardsCount - i - 1;
                        toreturn.Add(Cards.ElementAt(index));
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }

            return toreturn;
        }

        public void SortCards(Func<ICard, IComparable> comparer)
        {
            this._cards = Cards.OrderBy(comparer).ToList();
        }

        public virtual int TotalCardsAvailable
        {
            get { return Cards.Count; }
        }
    }
}