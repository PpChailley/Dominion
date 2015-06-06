using System.Collections.Generic;
using System.Linq;

namespace org.gbd.Dominion.Model.GameMechanics
{
    public class Resources
    {

        public int Money;
        public int Potions;

        public Resources(int money, int potions = 0)
        {
            Money = money;
            Potions = potions;
        }

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
    }
}