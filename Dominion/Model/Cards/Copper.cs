using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.gbd.Dominion.Model.Cards
{
    class Copper: Card
    {
        public Copper()
        {
            Mechanics.Cost = new Resources(0);
            Mechanics.BuyValue = new Resources(1);
            Mechanics.Types.Add(CardType.Treasure);
        }
    }
}
