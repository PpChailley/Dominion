using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Model.GameMechanics.AI
{
    /// <summary>
    /// Discards cards based on their buying cost, and always the minimum amount
    /// </summary>
    internal class ValueDiscardDelegate : AbstractAiSpecializedDelegate, IDiscardDelegate
    {

        public IList<ICard> Choose(int minAmount, int? maxAmount)
        {
            var discardable = Ai.Player.Deck.Hand.Cards.ToList();

            if (discardable.Count < minAmount)
            {
                throw new ArgumentOutOfRangeException("minAmount");
            }

            var valueDelegate = new Func<ICard, int>(c => c.Mechanics.Cost.AiValue());


            var toDiscard = new List<ICard>();
            while (toDiscard.Count < minAmount)
            {
                int minValue = discardable.Min(valueDelegate);
                var selected = Ai.Player.Deck.Hand.Cards.First(c => valueDelegate(c) == minValue); 
                toDiscard.Add(selected);
                discardable.Remove(selected);
            }

            return toDiscard;
        }
    }
}