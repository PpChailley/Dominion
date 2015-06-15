using System.Collections.Generic;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model
{
    public interface IHand: IZone

    {
        void Add(ICollection<ICard> cards);
        void Add(ICard cards);
    }
}