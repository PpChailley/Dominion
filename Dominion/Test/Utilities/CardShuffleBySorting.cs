using System.Linq;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Test.Utilities
{
    public class CardShuffleBySorting: ICardShuffler
    {
        public void Shuffle(IMutableZone toShuffle)
        {
            var cards = toShuffle.Cards.OrderBy(c => c.ToString());
            toShuffle.Cards = cards.ToList();
        }
    }
}