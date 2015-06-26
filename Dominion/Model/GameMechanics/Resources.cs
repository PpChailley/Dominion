using System;
using gbd.Tools.Clr;

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

        public void Pay(Resources price)
        {
            // TODO: test this line
            if (Money < price.Money || Potions < price.Potions)
                throw new InsufficientResourcesException("Cannot pay {0} with {1}"
                    .Format(price, this));

            Money -= price.Money;
            Potions -= price.Potions;

        }

        public override string ToString()
        {
            return String.Format("{{ {0} Coin - {1} Potion }}", Money, Potions);
        }
    }
}