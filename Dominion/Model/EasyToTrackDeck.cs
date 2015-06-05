using org.gbd.Dominion.Contents;

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