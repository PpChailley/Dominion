using System.Collections.Generic;

namespace org.gbd.Dominion.Model
{
    public class CardMechanics
    {
        public Cost cost;
        public List<GameAction> OnBuyTrigger = new List<GameAction>();
        public List<GameAction> OnPlayTrigger = new List<GameAction>();



    }
}