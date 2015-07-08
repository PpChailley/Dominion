using System.Collections.Generic;
using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Model.GameMechanics.AI
{
    public interface IReceiveDelegate : IAiSpecializedDelegate
    {
        IList<ICard> Choose(Resources minCost, Resources maxCost);
    }
}