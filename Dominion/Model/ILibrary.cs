using System.Collections;
using System.Collections.Generic;

namespace org.gbd.Dominion.Model
{
    public interface ILibrary
    {
        IList<ICard> Cards { get;  }
        IEnumerable<ICard> GetFromTop(int amount);
        void Add(ICard card, PositionInCardsCollection position);
    }
}