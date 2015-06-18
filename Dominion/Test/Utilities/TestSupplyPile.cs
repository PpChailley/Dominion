using System.Collections.Generic;
using gbd.Dominion.Model;
using gbd.Dominion.Model.Zones;
using Ninject;

namespace gbd.Dominion.Test.Utilities
{
    public class TestSupplyPile: SupplyPile, ISupplyPile
    {
        [Inject]
        public TestSupplyPile(IList<ICard> cards) : base(cards)
        {
        }
    }
}
