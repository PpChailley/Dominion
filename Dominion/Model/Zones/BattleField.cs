using System.Collections.Generic;
using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Model.Zones
{
    public class BattleField : AbstractZone, IBattleField
    {
        public BattleField(IList<ICard> cards) : base(cards) {}
        public BattleField() : base(new List<ICard>()) {}

        public new IList<ICard> Cards
        {
            get { return base.Cards; }
            set { base.Cards = value; }
        }
    }
}