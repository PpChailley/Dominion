using System.Collections.Generic;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;
using Ninject;

namespace gbd.Dominion.Model.GameMechanics
{
    public class TrashZone: AbstractZone, ITrashZone
    {
        [Inject]
        public TrashZone(IList<ICard> cards) : base(cards) { }

        public new IList<ICard> Cards { get { return base.Cards; }
                                        set { base.Cards = value;  } }

    }
}
