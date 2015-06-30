using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.GameMechanics
{
    public interface ICardShuffler
    {
        void Shuffle(IMutableZone toShuffle);
    }
}