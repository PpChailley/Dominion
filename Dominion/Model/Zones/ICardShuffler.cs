namespace gbd.Dominion.Model.Zones
{
    public interface ICardShuffler
    {
        void Shuffle(IMutableZone toShuffle);
    }
}