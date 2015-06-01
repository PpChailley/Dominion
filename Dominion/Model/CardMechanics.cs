using System.Collections.Generic;
using org.gbd.Dominion.Model.Actions;

namespace org.gbd.Dominion.Model
{
    public class CardMechanics
    {
        public Cost Cost;
        public List<IGameAction> OnBuyTrigger = new List<IGameAction>();
        public List<IGameAction> OnPlayTrigger = new List<IGameAction>();



    }
}