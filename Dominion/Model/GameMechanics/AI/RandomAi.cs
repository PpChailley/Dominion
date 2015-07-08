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
        
        // TODO : better testing if we inject a custom RandomProvider
        private readonly Random _rnd = new Random();



        public IList<ICard> Discard(int minAmount, int? maxAmount = null)
        {
            if (minAmount > Player.Deck.Hand.Cards.Count)
                throw new NotEnoughCardsException();

            int actualMax = Math.Min(maxAmount ?? minAmount, Player.Deck.Hand.Cards.Count);
            int amount = _rnd.Next(minAmount, actualMax);
            var cards =  Player.Deck.Hand.Cards.Random(amount);

            Log.Info("Discarding {0} cards (between {1} and {2}", amount, minAmount, maxAmount);

            cards.ToList().ForEach(c => 
                Log.Info("Choose to discard {0}", c));

            cards.MoveTo(Player.Deck.DiscardPile);
            return cards.ToList();
        }

        public IList<ICard> Receive(Resources minCost, Resources maxCost)
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

            return new[]{card};
        }

        public void PlayTurn()
        {
            Log.Info("Starting AI turn: Action phase");
            PlayActionsUntilCannot();
            IoC.Kernel.Get<IGame>().EndActionsPhase();

            Log.Info("Starting Buy phase");
            PlayAllTreasures();
            BuyUntilCannot();
            Log.Info("AI turn over");
        }

        private void PlayAllTreasures()
        {
            var treasures = Player.Deck.Hand.Cards.Where(c => c.Mechanics.GetCardType<TreasureType>() != null);

            treasures.ToList().ForEach(t => Player.PlayTreasure(t));
        }

        public IList<ICard> Buy()
        {
            var toBuy = new List<ICard>();

            while (toBuy.Count <= Player.Status.Buys)
            {
                var supply = IoC.Kernel.Get<IGame>().SupplyZone;
                var possibleBuys = supply.Piles.Where(p =>
                    p.Cards.Any(c => 
                        c.Mechanics.Cost.SmallerOrEqual(Player.Status.Resources))).ToList();

                if (possibleBuys.Any() == false)
                {
                    return toBuy;
                }

                var chosen = possibleBuys.Random().Cards.First();
                Log.Info("{0} ({2} buys left) choses to buy {1}", this, chosen, Player.Status.Buys);
                toBuy.Add(chosen);
            }

            Player.Buy(toBuy);
            return toBuy;
        }

        private void PlayActionsUntilCannot()
        {
            while (Player.Status.Actions > 0)
            {
                var actions = Player.Deck.Hand.Cards.Where(c =>
                    c.Mechanics.GetCardType<ActionType>() != null).ToList();

                if (actions.Any() == false)
                    break;

                var chosen = actions.Random();
                Log.Info("{0} ({2} actions left) choses to play {1}", this, chosen, Player.Status.Actions);
                Player.PlayAction(chosen);
            }
        }

        public override IList<ICard> Trash(Type t, ZoneChoice zoneFrom, int minAmount, int? maxAmount = null)
        {
            IZone from = Player.Deck.Get(zoneFrom);
            int actualMax = Math.Min(maxAmount ?? minAmount, from.Cards.Count);

            if (minAmount > from.Cards.Count)
                throw new NotEnoughCardsException(from, from.Cards.Count, minAmount);

            var pickableCards = from.Cards.Where(card => t.IsInstanceOfType(card)).ToList();
            if (pickableCards.Count < minAmount)
                throw new NotEnoughCardsException("Not enough cards match type constraint");

            int amount = _rnd.Next(minAmount, actualMax);
            var cards = pickableCards.Random(amount);

            Log.Info("Trashing {0} cards (between {1} and {2}", amount, minAmount, maxAmount);

            cards.ToList().ForEach(c =>
                Log.Info("Choose to trash {0}", c));

            cards.MoveTo(IoC.Kernel.Get<IGame>().Trash);

            return cards.ToList();
        }

    }
}