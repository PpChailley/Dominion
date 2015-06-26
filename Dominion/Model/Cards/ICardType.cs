using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.Cards
{
    public interface ICardType
    {
        void Ready(IZone currentlyPresentIn);
    }
}