using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test;

namespace gbd.Dominion.Model
{
    public class EasyToTrackDeck : AbstractDeck, IDeck
    {
        public EasyToTrackDeck()
        {
            for (var i = 0; i < 10; i++)
                DiscardPile.Cards.Add(new TestCard());
        }
    }
}