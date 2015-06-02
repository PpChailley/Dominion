using System.Collections.Generic;

namespace org.gbd.Dominion.Model
{
    public interface IPlayZone
    {
        IList<ICard> Cards { get; }
    }
}