using System;
using System.Collections.Generic;
using System.Linq;

namespace org.gbd.Dominion.Model
{
    public interface IZone
    {

        IList<ICard> Cards { get; }
        IEnumerable<ICard> Get(int amount, Position positionFrom);

        void SortCards(Func<ICard, IComparable> comparer);
    }
}