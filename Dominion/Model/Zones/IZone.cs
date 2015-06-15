using System;
using System.Collections.Generic;
using gbd.Dominion.Model;

namespace gbd.Dominion.Model.Zones
{
    public interface IZone
    {

        IList<ICard> Cards { get; }
        IEnumerable<ICard> Get(int amount, Position positionFrom = Position.Top);

        void SortCards(Func<ICard, IComparable> comparer);

        int TotalCardsAvailable { get; }
    }
}