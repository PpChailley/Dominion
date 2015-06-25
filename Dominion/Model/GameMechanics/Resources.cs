using System;
using System.Collections.Generic;
using System.Linq;

namespace gbd.Dominion.Model.GameMechanics
{
    public class Resources
    {

        public int Money;
        public int Potions;

        public Resources(int money, int potions)
        {
            Money = money;
            Potions = potions;
        }

        public Resources(int money): this(money, 0){ }



        public void Reset()
        {
            Money = 0;
            Potions = 0;
        }

        public void Pay(IEnumerable<Resources> res)
        {
            Money -= res.Aggregate(0, (current, r) => current + r.Money);
            Potions -= res.Aggregate(0, (current, r) => current + r.Potions);
        }

        public override string ToString()
        {
            return String.Format("{{ {0} Coin - {1} Potion }}", Money, Potions);
        }
    }
}