using System.Linq;
using Ninject;
using NUnit.Framework;
using org.gbd.Dominion.Contents;
using org.gbd.Dominion.Model;
using org.gbd.Dominion.Model.GameMechanics;
using org.gbd.Dominion.Model.Zones;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.Test
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

   


        [TestCase(0, 0, false, 5, 5, 0)]
        [TestCase(5, 2, false, 8, 0, 2)]
        [TestCase(5, 2, true, 8, 2, 0)]
        public void DiscardFromHand(int draw, int discard, bool shuffle, int expectInHand, int expectInLib, int expectInDiscard)
        {
            IoC.ReBind<IDeck>().To<StartingDeck>();

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
            IoC.ReBind<IDeck>().To<StartingDeck>();

            var player = IoC.Kernel.Get<Player>();
            player.GetReadyToStartGame();

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
            IoC.ReBind<IDeck>().To<StartingDeck>();

            var player = IoC.Kernel.Get<Player>();
            player.GetReadyToStartGame();


            player.Draw(6);
        }

        [Test]
        public void PlayerGainCard()
        {
            IoC.ReBind<IDeck>().To<StartingDeck>();

            var player = new Player();
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
            IoC.ReBind<IDeck>().To<StartingDeck>();

            var player = new Player();

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
            IoC.ReBind<IDeck>().To<EasyToTrackDeck>();

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
            IoC.ReBind<IDeck>().To<StartingDeck>();

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
            IoC.ReBind<IDeck>().To<EasyToTrackDeck>();

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
            IoC.ReBind<IDeck>().To<EasyToTrackDeck>();

            var deck = IoC.Kernel.Get<IDeck>();
            Assert.That(deck.Cards.Count, Is.EqualTo(10));

            deck.Library.Get(11);

        }
        
    }
}
