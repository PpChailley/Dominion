using System;
using System.Collections.Generic;
using System.Linq;

namespace org.gbd.Dominion.Model
{
    public class Game
    {
        public static void MoveCards(IEnumerable<ICard> toMove, IZone from, IZone to, PositionInCardsCollection positionInTargetCollection)
        {
            foreach (var card in toMove)
            {
                if (from.Cards.Contains(card) == false)
                    throw new InvalidOperationException("Card ");

                from.Cards.Remove(card);
                to.Cards.Add(card);
                card.ClearInPlayAttributes();
            }
            

        }
    }
}