using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Model.Cards

{
    public class TreasureType : ICardType
    {
        public Resources BuyValue;

        public TreasureType(Resources res)
        {
            BuyValue = res;
        }

        public TreasureType(int coins)
        {
            BuyValue = new Resources(coins);
        }
    }
}