using System.Collections.Generic;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.GameMechanics
{
    public interface IIntelligence
    {
        IList<ICard> Discard(int minAmount, int? maxAmount = null);

        IList<ICard> Receive(Resources minCost, Resources maxCost = null);

        IList<ICard> Trash<T>(ZoneChoice fromZone, int minAmount, int? maxAmount = null);

        void Ready(IPlayer player);

        IPlayer Player { get; }
        void PlayTurn();
    }
}