using System;
using System.Collections.Generic;
using System.Linq;

namespace org.gbd.Dominion.Model
{
    public class Game
    {
        public static int MoveCards(IEnumerable<ICard> toMove, IZone from, IZone to, Position positionInTargetCollection)
        {
            foreach (var card in toMove.ToList())
            {
                if (from.Cards.Contains(card) == false)
                    throw new InvalidOperationException(String.Format("Card {0} is not in source collection {1}", card, from));

                from.Cards.Remove(card);
                to.Cards.Add(card);
                card.ClearInPlayAttributes();
            }
            
            return toMove.Count();
        }

        public static void MoveCards(IZone from, IZone to, int amount = 1, Position positionFrom = Position.Top, Position positionTo = Position.Top)
        {
            MoveCards(from.Get(amount, positionFrom), from, to, positionTo);
        }

        public static void MoveCards(ILibrary from, IZone to, int amount = 1, Position positionFrom = Position.Top, Position positionTo = Position.Top)
        {
            if (from.TotalCardsAvailable < amount)
                throw new NotEnoughCardsException();

            if (from.Cards.Count < amount)
            {
                int toMoveAfterShuffle = amount - from.Cards.Count;
                MoveCards(from.Get(from.Cards.Count, positionFrom), from, to, positionTo);
                from.ShuffleDiscardToLibrary();
                MoveCards(from.Get(toMoveAfterShuffle, positionFrom), from, to, positionTo);
            }
            else
            {
                MoveCards(from.Get(amount, positionFrom), from, to, positionTo);
            }
        }


    }
}