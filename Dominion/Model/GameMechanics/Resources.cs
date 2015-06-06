using System.Collections.Generic;
using System.Linq;

namespace org.gbd.Dominion.Model
{
    public class Resources
    {

        public uint Money;
        public uint Potions;

        public Resources(uint money, uint potions = 0)
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
            Money -= res.Aggregate<Resources, uint>(0, (current, r) => current + r.Money);
            Potions -= res.Aggregate<Resources, uint>(0, (current, r) => current + r.Potions);
        }
    }
}