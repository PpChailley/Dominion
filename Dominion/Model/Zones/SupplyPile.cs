using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Tools;
using Ninject;

namespace gbd.Dominion.Model.Zones
{
    public class SupplyPile : AbstractZone, ISupplyPile 
    {

        public const int DEFAULT_SUPPLY_PILE_SIZE = 10;


        [Inject]
        public SupplyPile(IList<ICard> cards) : base(cards)
        {
           
        }

    }
}