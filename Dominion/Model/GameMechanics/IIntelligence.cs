using System.Collections.Generic;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Model
{
    public interface IIntelligence
    {
        IEnumerable<ICard> ChooseAndDiscard(int amount);

        void Init(Player player);

        Player Player { get; }
    }
}