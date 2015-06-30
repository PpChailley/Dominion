using System;
using System.Collections.Generic;
using gbd.Dominion.Model.Cards;
using gbd.Tools.Clr;

namespace gbd.Dominion.Model.GameMechanics.AI
{
    public class RandomAi : AbstractIntelligence, IIntelligence, IAi
    {
        private Random _rnd = new Random();


        public IEnumerable<ICard> ChooseAndDiscard(int amount)
        {
            return Player.Deck.Hand.Cards.Random(amount);
        }

        public IEnumerable<ICard> ChooseAndDiscard(int minAmount, int maxAmount)
        {
            int amount = _rnd.Next(minAmount, maxAmount);
            return Player.Deck.Hand.Cards.Random(amount);
        }


        
    }
}