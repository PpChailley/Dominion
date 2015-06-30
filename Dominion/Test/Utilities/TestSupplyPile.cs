using System.Collections.Generic;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;
using Ninject;

namespace gbd.Dominion.Test.Utilities
{
    // TODO: remove this useless class
    public class TestSupplyPile: SupplyPile, ISupplyPile
    {
        [Inject]
        public TestSupplyPile(IList<ICard> cards) : base(cards)
        {
        }
    }
}
