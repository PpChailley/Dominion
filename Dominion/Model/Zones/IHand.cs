using System.Collections.Generic;
using org.gbd.Dominion.Model.Zones;

namespace org.gbd.Dominion.Model
{
    public interface IHand: IZone

    {
        void Add(ICollection<ICard> cards);
        void Add(ICard cards);
    }
}