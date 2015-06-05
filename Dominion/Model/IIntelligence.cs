using System.Collections.Generic;

namespace org.gbd.Dominion.Model
{
    public interface IIntelligence
    {
        IEnumerable<ICard> ChooseAndDiscard(int amount);

        void Init(Player player);

        Player Player { get; }
    }
}