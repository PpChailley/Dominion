using System.Collections.Generic;
using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Model.Zones
{
    public class BattleField : AbstractZone, IBattleField
    {
        public BattleField(IList<ICard> cards) : base(cards) {}
        public BattleField() : base(new List<ICard>()) {}
    }
}