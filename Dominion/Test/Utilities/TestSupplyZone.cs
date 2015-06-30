using System;
using System.Collections.Generic;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Test.Utilities
{
    public class TestSupplyZone : SupplyZone, ISupplyZone
    {
        public TestSupplyZone(IList<ISupplyPile> piles) : base(piles)
        {
        }
    }
}
