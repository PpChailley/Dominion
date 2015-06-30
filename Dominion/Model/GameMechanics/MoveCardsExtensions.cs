using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.GameMechanics
{
    public static class MoveCardsExtensions
    {
        public static void MoveTo(this ICard card, IZone to, Position positionInTarget = Position.Top)
        {
            card.Zone.Cards.Remove(card);
            to.PutCard(card, positionInTarget);

            card.Attributes.Clear();
            card.Zone = to;
        }


         private static void PutCard(this IZone zone, ICard card, Position position = Position.Top)
        {
            switch (position)
            {
                case Position.Top:
                    zone.Cards.Add(card);
                    break;

                case Position.Bottom:
                    zone.Cards.Insert(0, card);
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }


        public static void MoveTo(this IEnumerable<ICard> toMove, IZone to, Position positionInTarget = Position.Top)
        {
            // TODO: Add robustness tests to MoveTo with 0 cards
            toMove.ToList().ForEach(c => c.MoveTo(to, positionInTarget));
        }


        public static void MoveCardsTo(this IZone from, IZone to, int amount, Position positionFrom = Position.Top, Position positionTo = Position.Top)
        {
            // TODO: Card.Tostring should show current zone
            // TODO: cleanup Library.PutCard
            from.Get(amount, positionFrom).MoveTo(to, positionTo);
        }

    }
}