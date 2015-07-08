using System.Collections.Generic;
using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Model.GameMechanics.AI
{
    public interface IDiscardDelegate : IAiSpecializedDelegate
    {
        IList<ICard> Choose(int minAmount, int? maxAmount);
    }
}