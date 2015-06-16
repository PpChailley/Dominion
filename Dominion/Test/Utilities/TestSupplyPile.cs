using System.Collections.Generic;
using gbd.Dominion.Model;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Tools;

namespace gbd.Dominion.Test.Utilities
{
    public class TestSupplyPile: SupplyPile, ISupplyPile
    {
        public TestSupplyPile(IList<ICard> cards) : base(cards)
        {
        }
    }
}
