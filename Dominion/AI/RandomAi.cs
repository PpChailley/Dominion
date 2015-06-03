using System;
using System.Collections.Generic;
using org.gbd.Dominion.Model;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.AI
{
    public class RandomAi : IIntelligence, IAi
    {
        private Random _rnd = new Random();

        public void Init(Player player)
        {
            Player = player;
        }

        public Player Player { get; private set; }

        public IEnumerable<ICard> ChooseAndDiscard(int amount)
        {
            return Player.Hand.Cards.Random(amount);
        }
    }
}