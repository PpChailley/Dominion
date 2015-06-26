using gbd.Dominion.Model.Zones;
using gbd.Tools.Clr;

namespace gbd.Dominion.Model.GameMechanics.Actions
{
    public class CardShufflerRandom: ICardShuffler
    {
        public void Shuffle(IMutableZone toShuffle)
        {
            toShuffle.Cards.Shuffle();
        }
    }
}