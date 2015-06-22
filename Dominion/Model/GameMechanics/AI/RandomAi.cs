using System;
using System.Collections.Generic;
using gbd.Dominion.Model.Cards;
using gbd.Tools.Cli;

namespace gbd.Dominion.Model.GameMechanics.AI
{
    public class RandomAi : AbstractIntelligence, IIntelligence, IAi
    {
        private Random _rnd = new Random();


        public override IEnumerable<ICard> ChooseAndDiscard(int amount)
        {
            return Player.Deck.Hand.Cards.Random(amount);
        }


        
    }
}