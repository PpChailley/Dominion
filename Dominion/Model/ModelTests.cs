using System.Collections.Generic;
using System.Linq;
using Ninject;
using NUnit.Framework;
using org.gbd.Dominion.Model.Actions;

namespace org.gbd.Dominion.Model
{
    [TestFixture]
    public class ModelTests: BaseTest
    {

        public const int NB_CARDS_IN_DEFAULT_DECK = 10;



        [Test]
        public void ShuffleDeck()
        {

            Kernel.Bind<IDeck>().To<StartingDeck>();

            var deck = Kernel.Get<IDeck>();
            var library = deck.Shuffle();

            Assert.That(deck.Cards.Count(), Is.EqualTo(10));
            Assert.That(library.Cards.Count(), Is.EqualTo(10));


        }


        [TestCase(0,0,10)]
        [TestCase(1,1,9)]
        [TestCase(2,2,8)]
        [TestCase(5,5,5)]
        [TestCase(10,10,0)]
        [TestCase(11,10,0)]
        [TestCase(199,10,0)]
        public void PlayerDraw(int amountToDraw, int expectedInHand, int expectedInLibrary)
        {
            Kernel.Bind<IDeck>().To<StartingDeck>();

            var player = new Player();

            Assert.That(player.Deck.Cards.Count(), Is.EqualTo(NB_CARDS_IN_DEFAULT_DECK));
            Assert.That(player.Library.Cards.Count(), Is.EqualTo(NB_CARDS_IN_DEFAULT_DECK));

            player.Draw(amountToDraw);

            Assert.That(player.Deck.Cards.Count(), Is.EqualTo(NB_CARDS_IN_DEFAULT_DECK));
            Assert.That(player.Library.Cards.Count(), Is.EqualTo(expectedInLibrary));
            Assert.That(player.Hand.Cards.Count(), Is.EqualTo(expectedInHand));

        }

        
    }
}
