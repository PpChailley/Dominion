using System.Collections.Generic;

namespace org.gbd.Dominion.Model
{
    public interface IHand

    {
        void Add(ICollection<ICard> cards);
        void Add(ICard cards);

        IList<ICard> Cards { get;  }
    }
}