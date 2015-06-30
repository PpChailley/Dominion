using gbd.Dominion.Model.Zones;
using gbd.Tools.Clr;

namespace gbd.Dominion.Model.GameMechanics
{
    public class CardShufflerRandom: ICardShuffler
    {
        public void Shuffle(IMutableZone toShuffle)
        {
            toShuffle.Cards.Shuffle();
        }
    }
}