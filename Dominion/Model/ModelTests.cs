using System.Collections.Generic;
using System.Linq;
using Ninject;
using NUnit.Framework;
using org.gbd.Dominion.Model.Actions;
using org.gbd.Dominion.Model.Cards;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.Model
{
    [TestFixture]
    public class ModelTests: BaseTest
    {

        public const int NB_CARDS_IN_DEFAULT_DECK = 10;



        [Test]
        public void ShuffleDeck()
        {
            IoC.ReBind<IDeck>().To<StartingDeck>();

            var deck = IoC.Kernel.Get<IDeck>();
            var library = deck.ShuffleDiscardToLibrary();

            Assert.That(deck.Cards.Count, Is.EqualTo(10));
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
            IoC.ReBind<IDeck>().To<StartingDeck>();

            var player = IoC.Kernel.Get<Player>();
            player.GetReadyToStartGame();

            Assert.That(player.Deck.Cards.Count(), Is.EqualTo(NB_CARDS_IN_DEFAULT_DECK));
            Assert.That(player.Library.Cards.Count(), Is.EqualTo(NB_CARDS_IN_DEFAULT_DECK));

            player.Draw(amountToDraw);

            Assert.That(player.Deck.Cards.Count(), Is.EqualTo(NB_CARDS_IN_DEFAULT_DECK));
            Assert.That(player.Library.Cards.Count(), Is.EqualTo(expectedInLibrary));
            Assert.That(player.Hand.Cards.Count(), Is.EqualTo(expectedInHand));

        }

        [Test]
        public void PlayerGainCard()
        {
            IoC.ReBind<IDeck>().To<StartingDeck>();

            var player = new Player();
            player.GetReadyToStartGame();

            Assert.That(player.CurrentScore, Is.EqualTo(0));

            player.Gain(new Estate());

            Assert.That(player.CurrentScore, Is.EqualTo(1));
            Assert.That(player.Deck.Cards.Count, Is.EqualTo(NB_CARDS_IN_DEFAULT_DECK + 1));
            Assert.That(player.Library.Cards.Count(), Is.EqualTo(NB_CARDS_IN_DEFAULT_DECK));

        }


        [Test]
        public void CountVictory()
        {
            IoC.ReBind<IDeck>().To<StartingDeck>();

            var player = new Player();

            Assert.That(player.CurrentScore, Is.EqualTo(0));

            player.Gain(new Estate());
            Assert.That(player.CurrentScore, Is.EqualTo(1));


            player.Gain(new Duchy());
            Assert.That(player.CurrentScore, Is.EqualTo(4));

            player.Gain(new Province());
            Assert.That(player.CurrentScore, Is.EqualTo(10));



        }

        
    }
}
