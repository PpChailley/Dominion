﻿using System;
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
        private static readonly ILogger _log = LogManager.GetCurrentClassLogger();
        
        private Random _rnd = new Random();


        public IEnumerable<ICard> Discard(int amount)
        {
            return Player.Deck.Hand.Cards.Random(amount);
        }


        public IEnumerable<ICard> Discard(int minAmount, int? maxAmount = null)
        {
            int amount = _rnd.Next(minAmount, maxAmount ?? minAmount);
            return Player.Deck.Hand.Cards.Random(amount);
        }

        public ICard Receive(Resources minCost, Resources maxCost)
        {
            var card = IoC.Kernel.Get<IGame>().SupplyZone.Cards
                .Where(c => c.Mechanics.Cost.GreaterOrEqual(minCost)
                            && c.Mechanics.Cost.SmallerOrEqual(maxCost))
                .ToList()
                .Random();

            _log.Info("Choose to Receive {0}", card);
            card.MoveTo(Player.Deck.DiscardPile);

            return card;
        }

        public IEnumerable<ICard> Trash<T>(ZoneChoice @from, int minAmount, int? maxAmount = null)
        {
            throw new NotImplementedException();
        }

    }
}