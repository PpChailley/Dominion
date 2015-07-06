using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;
using gbd.Tools.Clr;
using NLog;

namespace gbd.Dominion.Model.GameMechanics
{
    internal static class MoveCardsExtensions
    {
        private static readonly ILogger _log = LogManager.GetCurrentClassLogger();

        internal static void MoveTo(this ICard card, IZone to, Position positionInTarget = Position.Top)
        {
            if (card.Zone == to)
            {
                throw new InvalidOperationException("Cannot move card {0} to the same zone {1}".Format(card, to));
            }
            _log.Debug("Move {0} from {1} to {2}", card, card.Zone, to);

            card.Zone.Cards.Remove(card);
            to.PutCard(card, positionInTarget);

            card.Attributes.Clear();
            card.Zone = to;
        }


        internal static void PutCard(this IZone zone, ICard card, Position position = Position.Top)
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


        internal static void MoveTo(this IEnumerable<ICard> toMove, IZone to, Position positionInTarget = Position.Top)
        {
            // TODO: Add robustness tests to MoveTo with 0 cards
            toMove.ToList().ForEach(c => c.MoveTo(to, positionInTarget));
        }


        internal static void MoveCardsTo(this IZone from, IZone to, int amount, Position positionFrom = Position.Top, Position positionTo = Position.Top)
        {
            from.Get(amount, positionFrom).MoveTo(to, positionTo);
        }

    }
}