using System;
using System.Collections.Generic;
using org.gbd.Dominion.Model;

namespace org.gbd.Dominion.AI
{
    public abstract class AbstractIntelligence: IIntelligence
    {

        public Player Player { get; protected set; }



        public void Init(Player player)
        {
            Player = player;
        }
        
        
        public abstract IEnumerable<ICard> ChooseAndDiscard(int amount);

    }
}