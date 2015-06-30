using System;

namespace gbd.Dominion.Model.Zones
{
    public interface ISupplyPile : IMutableZone
    {
        Type CardType { get; }
    }
}