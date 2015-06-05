using System;
using System.Collections.Generic;
using org.gbd.Dominion.Model;
using org.gbd.Dominion.Model.GameMechanics.AI;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.AI
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