using System;
using System.Collections.Generic;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;
using gbd.Tools.Clr;

namespace gbd.Dominion.Model.GameMechanics.AI
{
    public class RandomAi : AbstractIntelligence, IIntelligence, IAi
    {
        private Random _rnd = new Random();


        public IEnumerable<ICard> Discard(int amount)
        {
            return Player.Deck.Hand.Cards.Random(amount);
        }

        public IEnumerable<ICard> Discard(int minAmount, int maxAmount)
        {
            int amount = _rnd.Next(minAmount, maxAmount);
            return Player.Deck.Hand.Cards.Random(amount);
        }


        public IEnumerable<ICard> Discard(int minAmount, int? maxAmount = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ICard> Receive(Resources minCost, Resources maxCost)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ICard> Trash<T>(ZoneChoice @from, int minAmount, int? maxAmount = null)
        {
            throw new NotImplementedException();
        }

    }
}