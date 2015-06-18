using System;
using System.Collections.Generic;
using gbd.Dominion.AI;
using gbd.Tools.Cli;

namespace gbd.Dominion.Model.GameMechanics.AI
{
    public class RandomAi : AbstractIntelligence, IIntelligence, IAi
    {
        private Random _rnd = new Random();


        public override IEnumerable<ICard> ChooseAndDiscard(int amount)
        {
            return Player.Hand.Cards.Random(amount);
        }


        
    }
}