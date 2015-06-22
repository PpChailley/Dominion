using System.Collections.Generic;
using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Model.Zones
{
    public interface IMutableZone: IZone
    {
        new IList<ICard> Cards { get; set; }
    }
}