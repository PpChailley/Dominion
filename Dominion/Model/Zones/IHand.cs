using System.Collections.Generic;
using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Model.Zones
{
    public interface IHand: IZone

    {
        void Add(ICollection<ICard> cards);
        void Add(ICard cards);
    }
}