using System.Collections.Generic;

namespace gbd.Dominion.Model.Zones
{
    public class BattleField : AbstractZone, IBattleField
    {
        public BattleField(IList<ICard> cards) : base(cards) {}
    }
}