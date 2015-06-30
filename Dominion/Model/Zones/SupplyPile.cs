using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model.Cards;
using Ninject;

namespace gbd.Dominion.Model.Zones
{
    public class SupplyPile : AbstractZone, ISupplyPile
    {


        public Type CardType { get; private set; }


        [Inject]
        public SupplyPile(IList<ICard> cards) : base(cards)
        {
            CardType = cards.Select(c => c.GetType()).Distinct().SingleOrDefault();
            cards.ToList().ForEach(c => c.Zone = this);
        }

        public new IList<ICard> Cards
        {
            get { return base.Cards; }
            set { base.Cards = value; }
        }


    }
}