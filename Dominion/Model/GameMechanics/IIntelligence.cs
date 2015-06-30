using System.Collections.Generic;
using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Model.GameMechanics
{
    public interface IIntelligence
    {
        IEnumerable<ICard> ChooseAndDiscard(int amount);
        IEnumerable<ICard> ChooseAndDiscard(int minAmount, int maxAmount);

        void Init(Player player);

        Player Player { get; }
    }
}