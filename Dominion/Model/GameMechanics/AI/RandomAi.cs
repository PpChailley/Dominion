using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Injection;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;
using gbd.Tools.Clr;
using Ninject;
using NLog;

namespace gbd.Dominion.Model.GameMechanics.AI
{
    public class RandomAi : AbstractIntelligence, IIntelligence, IAi
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();
        
        private readonly Random _rnd = new Random();



        public IEnumerable<ICard> Discard(int minAmount, int? maxAmount = null)
        {
            if (minAmount > Player.Deck.Hand.Cards.Count)
                throw new NotEnoughCardsException();

            int actualMax = Math.Min(maxAmount ?? minAmount, Player.Deck.Hand.Cards.Count);
            int amount = _rnd.Next(minAmount, actualMax);
            var cards =  Player.Deck.Hand.Cards.Random(amount);
            
            cards.ToList().ForEach(c => 
                Log.Info("Choose to discard {0}", c));

            cards.MoveTo(Player.Deck.DiscardPile);
            return cards;
        }

        public ICard Receive(Resources minCost, Resources maxCost)
        {
            var supply = IoC.Kernel.Get<IGame>().SupplyZone;

            if (supply.Cards.Count < 1)
                throw new NotEnoughCardsException("Supply is empty");

            var possibleCards = supply.Cards
                                .Where(c => c.Mechanics.Cost.GreaterOrEqual(minCost)
                                            && c.Mechanics.Cost.SmallerOrEqual(maxCost))
                                .ToList();

            if (possibleCards.Any() == false)
                throw new NotEnoughCardsException("Supply has no cards acceptable to pick");

            var card = possibleCards.Random();

            Log.Info("Choose to Receive {0}", card);
            card.MoveTo(Player.Deck.DiscardPile);

            return card;
        }

        public IEnumerable<ICard> Trash<T>(ZoneChoice @from, int minAmount, int? maxAmount = null)
        {
            throw new NotImplementedException();
        }

    }
}