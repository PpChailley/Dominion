namespace org.gbd.Dominion.Model
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
    }
}