using System;
using gbd.Tools.Clr;

namespace gbd.Dominion.Model.GameMechanics
{
    public class Resources
    {


        public override int GetHashCode()
        {
            unchecked
            {
                return (Money*397) ^ Potions;
            }
        }

        public int Money;
        public int Potions;

        public Resources(int money, int potions)
        {
            if (money < 0 || potions < 0)
                throw new InvalidOperationException("Resources amount must be nonnegative");

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

        public override bool Equals(object obj)
        {
            return  obj is Resources && 
                    ((Resources)obj).Money == this.Money && 
                    ((Resources)obj).Potions == this.Potions;
        }
        
        protected bool Equals(Resources other)
        {
            return Money == other.Money && Potions == other.Potions;
        }
    }
}