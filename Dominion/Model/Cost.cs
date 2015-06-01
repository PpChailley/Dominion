namespace org.gbd.Dominion.Model
{
    public class Cost
    {

        public int Money;
        public int Potions;

        public Cost(int money, int potions = 0)
        {
            Money = money;
            Potions = potions;
        }
    }
}