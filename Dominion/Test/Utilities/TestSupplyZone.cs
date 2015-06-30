using System;
using System.Collections.Generic;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Test.Utilities
{
    public class TestSupplyZone : SupplyZone, ISupplyZone
    {
        public TestSupplyZone(IEnumerable<ICard> cards) : base(cards)
        {
        }
    }
}
