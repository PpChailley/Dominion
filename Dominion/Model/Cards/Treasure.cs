namespace org.gbd.Dominion.Model.Cards

{
    public class Treasure : ICardType
    {
        public Resources BuyValue;

        public Treasure(Resources res)
        {
            BuyValue = res;
        }

        public Treasure(int coins)
        {
            BuyValue = new Resources(coins);
        }
    }
}