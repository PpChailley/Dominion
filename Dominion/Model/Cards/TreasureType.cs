using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.Cards

{
    public class TreasureType : ICardType
    {
        public Resources BuyValue;
        protected IZone Zone { get; set; }

        public TreasureType(Resources res)
        {
            BuyValue = res;
        }

        public TreasureType(int coins)
        {
            BuyValue = new Resources(coins);
        }

        public void Ready(IZone zone)
        {
            Zone = zone;
        }

        
    }
}