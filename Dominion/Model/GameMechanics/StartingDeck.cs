using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Contents;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.GameMechanics
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