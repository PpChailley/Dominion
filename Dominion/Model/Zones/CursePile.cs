using System.Collections.Generic;
using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Model.Zones
{
    public class CursePile: SupplyPile
    {
        public CursePile(IList<ICard> cards) : base(cards){}
    }
}