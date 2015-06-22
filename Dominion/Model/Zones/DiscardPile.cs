using System.Collections.Generic;
using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Model.Zones
{
    public class DiscardPile: AbstractZone, IDiscardPile
    {
        public DiscardPile(IList<ICard> cards) : base(cards) {}
        public DiscardPile() : base(new List<ICard>()) {}
    }
}