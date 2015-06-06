using org.gbd.Dominion.Contents;
using org.gbd.Dominion.Contents.Cards;
using org.gbd.Dominion.Model.Zones;

namespace org.gbd.Dominion.Model.GameMechanics
{
    public class StartingDeck : AbstractDeck, IDeck
    {
        public StartingDeck()
        {
            // TODO/ Ultimately this should be sent through NInject (and this class obsoleted?)

            for (var i = 0; i < 7; i++)
                DiscardPile.Cards.Add(new Copper());

            for (var i = 0; i < 3; i++)
                DiscardPile.Cards.Add(new Estate());

        }


    }
}