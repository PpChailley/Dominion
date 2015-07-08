using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.GameMechanics.AI
{
    /// <summary>
    /// Choses for trashing the minimum number of cards, selecting the cheapest
    /// </summary>
    internal class ValueTrashDelegate: AbstractAiSpecializedDelegate, ITrashDelegate
    {
        public IList<ICard> Choose(ZoneChoice fromZone, int minAmount, int? maxAmount)
        {
            // TODO: move Legal cards discrimination to abstract parent
            var legal = Ai.Player.Deck.Get(fromZone).Cards;

            var chosen = new List<ICard>();

            while (chosen.Count < minAmount)
            {
                var minPrice = legal.Min(c => AiHelper.AiValue(c.Mechanics.Cost));
                var card = legal.First(c => c.Mechanics.Cost.AiValue() == minPrice);
                chosen.Add(card);
            }

            return chosen;
        }
    }
}