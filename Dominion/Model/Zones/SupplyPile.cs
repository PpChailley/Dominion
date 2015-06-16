using System.Collections.Generic;
using gbd.Dominion.Tools;
using Ninject;

namespace gbd.Dominion.Model.Zones
{
    public class SupplyPile : AbstractZone, ISupplyPile 
    {

        public const int DEFAULT_SUPPLY_PILE_SIZE = 10;



        public SupplyPile(IList<ICard> cards) : base(cards) {}

    }
}