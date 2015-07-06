using System.Collections.Generic;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.GameMechanics
{
    public interface IIntelligence
    {
        IEnumerable<ICard> Discard(int minAmount, int? maxAmount = null);

        ICard Receive(Resources minCost, Resources maxCost);

        IEnumerable<ICard> Trash<T>(ZoneChoice from, int minAmount, int? maxAmount = null);

        void Ready(Player player);

        Player Player { get; }
        void PlayTurn();
    }
}