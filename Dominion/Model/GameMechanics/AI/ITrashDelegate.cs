using System.Collections.Generic;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.GameMechanics.AI
{
    public interface ITrashDelegate : IAiSpecializedDelegate
    {
        IList<ICard> Choose(ZoneChoice fromZone, int minAmount, int? maxAmount);
    }
}