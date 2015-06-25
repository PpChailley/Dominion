using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.Cards
{
    public abstract class CardType
    {
        protected IZone Zone { get; set; }

        public void Ready(IZone zone)
        {
            Zone = zone;
        }
    }
}