using System;
using System.Collections.Generic;

namespace org.gbd.Dominion.Model
{
    public interface IZone
    {

        IList<ICard> Cards { get; }
        IEnumerable<ICard> Get(int amount, Position positionFrom = Position.Top);

        void SortCards(Func<ICard, IComparable> comparer);

        int TotalCardsAvailable { get; }
    }
}