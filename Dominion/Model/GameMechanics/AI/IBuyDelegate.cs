using System.Collections.Generic;
using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Model.GameMechanics.AI
{
    public interface IBuyDelegate: IAiSpecializedDelegate
    {
        IList<ICard> Choose(Resources available);
    }
}