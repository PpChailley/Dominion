using System.Collections.Generic;

namespace org.gbd.Dominion.Model
{
    public interface IZone
    {

        IList<ICard> Cards { get; }
    }
}