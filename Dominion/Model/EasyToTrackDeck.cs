using org.gbd.Dominion.Model.Zones;
using org.gbd.Dominion.Test;

namespace org.gbd.Dominion.Model
{
    public class EasyToTrackDeck : Deck, IDeck
    {
        public EasyToTrackDeck()
        {
            for (var i = 0; i < 10; i++)
                DiscardPile.Cards.Add(new TestCard());
        }
    }
}