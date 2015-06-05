using System.Collections.Generic;

namespace org.gbd.Dominion.Model
{
    public interface IHand: IZone

    {
        void Add(ICollection<ICard> cards);
        void Add(ICard cards);
    }
}