using System;
using System.Collections.Generic;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Test.Utilities
{
    public class TestSupplyZone : AbstractSupplyZone, ISupplyZone
    {
        public TestSupplyZone(IList<ISupplyPile> piles, CursePile cursePile) : base(piles, cursePile)
        {
        }
    }
}
