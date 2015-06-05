using System.Collections.Generic;
using org.gbd.Dominion.Model.GameMechanics;

namespace org.gbd.Dominion.Model
{
    public interface IIntelligence
    {
        IEnumerable<ICard> ChooseAndDiscard(int amount);

        void Init(Player player);

        Player Player { get; }
    }
}