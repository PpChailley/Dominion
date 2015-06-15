using System;
using System.Collections.Generic;
using System.Linq;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.Model.Zones
{
    internal class SupplyPile: ISupplyPile
    {
        

        public IList<ICard> Cards { get; private set; }


        public SupplyPile()
        {
            Cards = new List<ICard>();
        }



        public IEnumerable<ICard> Get(int amount, Position positionFrom = Position.Top)
        {
            switch (positionFrom)
            {
                case Position.Top:
                    return Cards.Take(amount);

                case Position.Bottom:
                    return Cards.TakeLast(amount);

                default:
                    throw new InvalidOperationException();
            }
        }

        public void SortCards(Func<ICard, IComparable> comparer)
        {
            Cards = Cards.OrderBy(comparer).ToList();
        }

        public int TotalCardsAvailable
        {
            get { return Cards.Count; }
        }
    }
}