using System;
using System.Collections.Generic;

namespace gbd.Dominion.Model.Zones
{
    public interface ISupplyZone: IZone
    {
        IList<ISupplyPile> Piles { get; }
        bool EndOfGameCondition { get; }

        ISupplyPile PileOf<TCardType>();

        ISupplyPile PileOf(Type cardType);
    }
}