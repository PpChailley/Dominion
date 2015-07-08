using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Injection;
using gbd.Dominion.Model.Cards;
using Ninject;

namespace gbd.Dominion.Model.GameMechanics.AI
{
    /// <summary>
    /// Chooses to receive the most expensive card available
    /// </summary>
    internal class ValueReceiveDelegate : AbstractAiSpecializedDelegate, IReceiveDelegate
    {
        public IList<ICard> Choose(Resources minCost, Resources maxCost)
        {
            var legalPiles = IoC.Kernel.Get<IGame>().SupplyZone.Piles
                .Where(p => p.Cards.Any() &&
                            p.Cards.First().Mechanics.Cost.SmallerOrEqual(maxCost) &&
                            p.Cards.First().Mechanics.Cost.GreaterOrEqual(minCost))
                .ToList();

            var maxPrice = legalPiles.Max(p => p.Cards.First().Mechanics.Cost.AiValue());

            var chosen = legalPiles
                .First(p => p.Cards.First().Mechanics.Cost.AiValue() == maxPrice)
                .Cards.First();

            return new[] {chosen};
        }
    }
}