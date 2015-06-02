using System.Collections;
using System.Collections.Generic;

namespace org.gbd.Dominion.Model
{
    public interface ILibrary
    {
        IEnumerable<ICard> Cards { get;  }

        IEnumerable<ICard> Dequeue(int amount);
    }
}