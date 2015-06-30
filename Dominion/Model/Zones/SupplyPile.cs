using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model.Cards;
using Ninject;

namespace gbd.Dominion.Model.Zones
{
    public class SupplyPile : AbstractZone, ISupplyPile
    {

        // Todo: get rid of this constant
        public const int DEFAULT_SUPPLY_PILE_SIZE = 10;

        public Type CardType { get; private set; }


        [Inject]
        public SupplyPile(IList<ICard> cards) : base(cards)
        {
            CardType = cards.Select(c => c.GetType()).Distinct().SingleOrDefault();
        }

        public new IList<ICard> Cards
        {
            get { return base.Cards; }
            set { base.Cards = value; }
        }


    }
}