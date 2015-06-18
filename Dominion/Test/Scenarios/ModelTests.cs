using System.Linq;
using gbd.Dominion.Contents;
using gbd.Dominion.Model;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
using gbd.Dominion.Tools;
using gbd.Tools.NInject;
using Ninject;
using NUnit.Framework;

namespace gbd.Dominion.Test.Scenarios
{
    [TestFixture]
    public class ModelTests: BaseTest
    {

        public const int NB_CARDS_IN_DEFAULT_DECK = 10;

        [Test]
        public void Deck()
        {
            var deck = IoC.Kernel.Get<IDeck>();
            Assert.That(deck.Cards.Count, Is.EqualTo(10));
        }

        [Test]
        public void ShuffleDeck()
        {
            IoC.Kernel.ReBind<IDeck>().To<StartingDeck>();

            var deck = IoC.Kernel.Get<IDeck>();
            var library = deck.ShuffleDiscardToLibrary();

            Assert.That(deck.Cards.Count, Is.EqualTo(10));
            Assert.That(library.Cards.Count(), Is.EqualTo(10));
        }

   


        [TestCase(0, 0, false, 5, 5, 0)]
        [TestCase(5, 2, false, 8, 0, 2)]
        [TestCase(5, 2, true, 8, 2, 0)]
        public void DiscardFromHand(int draw, int discard, bool shuffle, int expectInHand, int expectInLib, int expectInDiscard)
        {
            IoC.Kernel.ReBind<IDeck>().To<StartingDeck>();

            var player = IoC.Kernel.Get<IPlayer>();
            player.GetReadyToStartGame();

            player.Draw(draw);
            player.DiscardFromHand(discard);

            if (shuffle)
                player.Deck.ShuffleDiscardToLibrary();

            Assert.That(player.Deck.Cards.Count, Is.EqualTo(NB_CARDS_IN_DEFAULT_DECK));
            Assert.That(player.CurrentScore, Is.EqualTo(3));

            Assert.That(player.Hand.Cards.Count, Is.EqualTo(expectInHand));
            Assert.That(player.DiscardPile.Cards.Count, Is.EqualTo(expectInDiscard));
            Assert.That(player.Library.Cards.Count, Is.EqualTo(expectInLib));
        }


        [TestCase(0,5,5)]
        [TestCase(1,6,4)]
        [TestCase(2,7,3)]
        [TestCase(5,10,0)]
        public void PlayerDraw(int amountToDraw, int expectedInHand, int expectedInLibrary)
        {
            var player = IoC.Kernel.Get<Player>();
            player.GetReadyToStartGame();

            Assert.That(IoC.Kernel.GetBindings(typeof(ICard)).Count(), Is.EqualTo(10));

            Assert.That(player.Deck.Cards.Count(), Is.EqualTo(NB_CARDS_IN_DEFAULT_DECK));
            Assert.That(player.Library.Cards.Count(), Is.EqualTo(5));

            player.Draw(amountToDraw);

            Assert.That(player.Deck.Cards.Count(), Is.EqualTo(NB_CARDS_IN_DEFAULT_DECK));
            Assert.That(player.Library.Cards.Count(), Is.EqualTo(expectedInLibrary));
            Assert.That(player.Hand.Cards.Count(), Is.EqualTo(expectedInHand));

        }

        [ExpectedException(typeof(NotEnoughCardsException))]
        [Test]
        public void PlayerDrawAllDeckPlusOne()
        {
            IoC.Kernel.ReBind<IDeck>().To<StartingDeck>();

            var player = IoC.Kernel.Get<Player>();
            player.GetReadyToStartGame();


            player.Draw(6);
        }

        [Test]
        public void PlayerGainCard()
        {
            IoC.Kernel.ReBind<IDeck>().To<StartingDeck>();

            var player = IoC.Kernel.Get<IPlayer>();
            player.GetReadyToStartGame();

            Assert.That(player.CurrentScore, Is.EqualTo(3));

            player.AddToDeck(new Estate());

            Assert.That(player.CurrentScore, Is.EqualTo(4));
            Assert.That(player.Deck.Cards.Count, Is.EqualTo(NB_CARDS_IN_DEFAULT_DECK + 1));
            Assert.That(player.Library.Cards.Count(), Is.EqualTo(NB_CARDS_IN_DEFAULT_DECK));

        }


        [Test]
        public void CountVictory()
        {
            IoC.Kernel.ReBind<IDeck>().To<StartingDeck>();

            var player = IoC.Kernel.Get<IPlayer>();

            Assert.That(player.CurrentScore, Is.EqualTo(3));

            player.AddToDeck(new Estate());
            Assert.That(player.CurrentScore, Is.EqualTo(4));


            player.AddToDeck(new Duchy());
            Assert.That(player.CurrentScore, Is.EqualTo(7));

            player.AddToDeck(new Province());
            Assert.That(player.CurrentScore, Is.EqualTo(13));



        }



        [Test]
        public void ReshuffleWhenEmpty()
        {
            IoC.Kernel.ReBind<IDeck>().To<TestDeck>();

            var player = IoC.Kernel.Get<Player>();
            player.GetReadyToStartGame();

            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5, 5, 0, 0)));

            Game.MoveCards(player.Hand, player.DiscardPile, 3);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5,2,3,0)));

            player.Draw(5);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(0, 7, 3, 0)));

            player.DiscardFromHand(3);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(0, 4, 6, 0)));

            player.Draw(3);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(3, 7, 0, 0)));

        }


        [Test, ExpectedException(typeof(NotEnoughCardsException))]
        public void ExceptionWhenDrawFromEmptyDeck()
        {
            IoC.Kernel.ReBind<IDeck>().To<StartingDeck>();

            var player = IoC.Kernel.Get<Player>();
            player.GetReadyToStartGame();

            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5, 5, 0, 0)));

            player.Draw(5);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(0, 10, 0, 0)));

            player.Draw(2);

        }

        [Test]
        [Explicit]
        public void CardGet()
        {
            IoC.Kernel.ReBind<IDeck>().To<TestDeck>();

            var player = IoC.Kernel.Get<Player>();

            player.Library.Init(player.Deck);
            player.Deck.Library.SortCards(card => ((TestCard) card).Index);


            var sample = player.Library.Get(1, Position.Top).ToList();

            Assert.That(sample.Count(), Is.EqualTo(1));
            Assert.That(((TestCard) sample.First()).Index, Is.EqualTo(1));
            Assert.That(((TestCard) sample.Last()).Index, Is.EqualTo(1));

            sample = player.Library.Get(5, Position.Top).ToList();

            Assert.That(sample.Count(), Is.EqualTo(5));
            Assert.That(((TestCard) sample.First()).Index, Is.EqualTo(1));
            Assert.That(((TestCard) sample.Last()).Index, Is.EqualTo(5));

            sample = player.Library.Get(1, Position.Bottom).ToList();

            Assert.That(sample.Count(), Is.EqualTo(1));
            Assert.That(((TestCard) sample.First()).Index, Is.EqualTo(10));
            Assert.That(((TestCard) sample.Last()).Index, Is.EqualTo(10));

            sample = player.Library.Get(4, Position.Bottom).ToList();

            Assert.That(sample.Count(), Is.EqualTo(4));
            Assert.That(((TestCard) sample.First()).Index, Is.EqualTo(10));
            Assert.That(((TestCard) sample.Last()).Index, Is.EqualTo(7));
     
        }


        [Test, ExpectedException(typeof(NotEnoughCardsException))]
        public void GetTooManyCardsFromZone()
        {
            IoC.Kernel.ReBind<IDeck>().To<TestDeck>();

            var deck = IoC.Kernel.Get<IDeck>();
            Assert.That(deck.Cards.Count, Is.EqualTo(10));

            deck.Library.Get(11);

        }
        
    }
}
