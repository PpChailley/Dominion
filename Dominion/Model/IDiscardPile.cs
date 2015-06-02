using System.Collections.Generic;

namespace org.gbd.Dominion.Model
{
    public interface IDiscardPile
    {
        ICollection<ICard> Cards { get; }
    }
}