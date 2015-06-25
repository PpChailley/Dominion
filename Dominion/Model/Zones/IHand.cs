using System.Collections.Generic;
using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Model.Zones
{
    public interface IHand: IMutableZone

    {
        void Add(ICollection<ICard> cards);
        void Add(ICard cards);
    }
}