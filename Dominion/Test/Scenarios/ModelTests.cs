using System;
using System.Linq;
using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Injection;
using gbd.Dominion.Model;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
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


        [TestCase(1)]
        [TestCase(4)]
        [TestCase(0)]
        [TestCase(100)]
        public void NoPlayersLeak(int nbPlayers)
        {
            IoC.Kernel.Unbind<IPlayer>();
            // TODO: rename and shorten this method
            IoC.Kernel.BindMultipleTimesTo<IPlayer, Player>(nbPlayers);

            var game = IoC.Kernel.Get<IGame>();

            Assert.That(game.Players, Has.Count.EqualTo(nbPlayers));
            Assert.That(game.GetPlayers(PlayerChoice.Opponents), Has.Count.EqualTo(Math.Max(nbPlayers - 1, 0)));

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
            player.Ready();

            player.Draw(draw);
            player.ChooseAndDiscard(discard);

            if (shuffle)
                player.Deck.ShuffleDiscardToLibrary();

            Assert.That(player.Deck.Cards.Count, Is.EqualTo(NB_CARDS_IN_DEFAULT_DECK));

            Assert.That(player.Deck.Hand.Cards.Count, Is.EqualTo(expectInHand));
            Assert.That(player.Deck.DiscardPile.Cards.Count, Is.EqualTo(expectInDiscard));
            Assert.That(player.Deck.Library.Cards.Count, Is.EqualTo(expectInLib));
        }


        [TestCase(0,5,5)]
        [TestCase(1,6,4)]
        [TestCase(2,7,3)]
        [TestCase(5,10,0)]
        public void PlayerDraw(int amountToDraw, int expectedInHand, int expectedInLibrary)
        {
            var player = IoC.Kernel.Get<Player>();
            player.Ready();


            Assert.That(player.Deck.Cards.Count(), Is.EqualTo(NB_CARDS_IN_DEFAULT_DECK));
            Assert.That(player.Deck.Library.Cards.Count(), Is.EqualTo(5));

            player.Draw(amountToDraw);

            Assert.That(player.Deck.Cards.Count(), Is.EqualTo(NB_CARDS_IN_DEFAULT_DECK));
            Assert.That(player.Deck.Library.Cards.Count(), Is.EqualTo(expectedInLibrary));
            Assert.That(player.Deck.Hand.Cards.Count(), Is.EqualTo(expectedInHand));

        }

        [ExpectedException(typeof(NotEnoughCardsException))]
        [Test]
        public void PlayerDrawAllDeckPlusOne()
        {
            IoC.Kernel.ReBind<IDeck>().To<StartingDeck>();

            var player = IoC.Kernel.Get<Player>();
            player.Ready();


            player.Draw(6);
        }

       

        [TestCase(0, 0)]
        [TestCase(3, 0)]
        [TestCase(0, 3)]
        [TestCase(10, 0)]
        [TestCase(7, 9)]
        public void CountVictory(int estates, int provinces)
        {
            IoC.Kernel.Unbind<ICard>();

            IoC.Kernel.BindCard<Estate, ISupplyZone>(100);
            IoC.Kernel.BindCard<Duchy, ISupplyZone>(100);
            IoC.Kernel.BindCard<Province, ISupplyZone>(100);

            IoC.Kernel.BindCard<Estate, ILibrary>(estates);
            IoC.Kernel.BindCard<Province, ILibrary>(provinces);
            IoC.Kernel.BindCard<Copper, ILibrary>(Math.Max(10 - estates - provinces, 3));

            var player = IoC.Kernel.Get<IPlayer>();
            player.Ready();
            player.StartTurn();

            var supply = IoC.Kernel.Get<ISupplyZone>();

            int expectedScore = 1 * estates + 6 * provinces;

            Assert.That(player.CurrentScore, Is.EqualTo(expectedScore));

            player.ReceiveFrom(supply.PileOf<Estate>(), 1);
            Assert.That(player.CurrentScore, Is.EqualTo(expectedScore + 1 ));


            player.ReceiveFrom(supply.PileOf<Duchy>(), 1);
            Assert.That(player.CurrentScore, Is.EqualTo(expectedScore + 4));

            player.ReceiveFrom(supply.PileOf<Province>(), 1);
            Assert.That(player.CurrentScore, Is.EqualTo(expectedScore + 10));

        }




        [TestCase(10,0,10)]
        [TestCase(10,10,15)]
        [TestCase(1,10,3)]
        public void LibraryReshuffle(int librarySize, int discardSize, int pick)
        {
            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.BindMultipleTimes<ICard>(librarySize).To<ICard, EmptyCard>()
                .WhenAnyAncestorOfType<EmptyCard, ILibrary>();
            IoC.Kernel.BindMultipleTimes<ICard>(discardSize).To<ICard, EmptyCard>()
                .WhenAnyAncestorOfType<EmptyCard, IDiscardPile>();


            var deck = IoC.Kernel.Get<IDeck>();
            deck.Ready();

            Assert.That(deck.CardCountByZone, Is.EqualTo(
                new CardRepartition(librarySize + discardSize ,0 , 0, 0)));

            deck.Library.MoveCardsTo(deck.Hand, pick);

            Assert.That(deck.CardCountByZone, Is.EqualTo(
                new CardRepartition(librarySize + discardSize - pick, pick, 0, 0)));
        }

 
  

        [Test]
        public void LibraryCanReshuffle()
        {
            var player = IoC.Kernel.Get<IPlayer>();
            player.Deck.Ready();

            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(10,0,0,0)));

            player.Deck.Library.MoveCardsTo(player.Deck.DiscardPile, 5);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5, 0, 5, 0)));

            player.Deck.Library.MoveCardsTo(player.Deck.Hand, 6);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(4, 6, 0, 0)));
        }

        [ExpectedException(typeof(NotEnoughCardsException))]
        [Test]
        public void DiscardCannotReshuffle()
        {
            var player = IoC.Kernel.Get<IPlayer>();
            player.Deck.Ready();

            player.Deck.Library.MoveCardsTo(player.Deck.DiscardPile, 5);
            player.Deck.DiscardPile.MoveCardsTo(player.Deck.Hand, 6);
        }

        [ExpectedException(typeof(NotEnoughCardsException))]
        [Test]
        public void HandCannotReshuffle()
        {
            var player = IoC.Kernel.Get<IPlayer>();
            player.Deck.Ready();

            player.Deck.Hand.MoveCardsTo(player.Deck.DiscardPile, 5);
        }

        [ExpectedException(typeof(NotEnoughCardsException))]
        [Test]
        public void BAttlefieldCannotReshuffle()
        {
            var player = IoC.Kernel.Get<IPlayer>();
            player.Deck.Ready();

            player.Deck.Library.MoveCardsTo(player.Deck.BattleField, 5);
            player.Deck.BattleField.MoveCardsTo(player.Deck.Hand, 6);
        }

        [ExpectedException(typeof(NotEnoughCardsException))]
        [Test]
        public void SupplyCannotReshuffle()
        {
            var player = IoC.Kernel.Get<IPlayer>();
            player.Deck.Ready();

            IoC.Kernel.Unbind<ISupplyPile>();
            IoC.Kernel.Bind<ISupplyPile>().To<SupplyPile>();
            var supply = IoC.Kernel.Get<ISupplyPile>();
            supply.Ready();

            supply.MoveCardsTo(player.Deck, 11);
        }



        [Test]
        public void ReshuffleWhenEmpty_FullCase()
        {
            IoC.Kernel.ReBind<IDeck>().To<TestDeck>();

            var player = IoC.Kernel.Get<Player>();
            player.Ready();

            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5, 5, 0, 0)));

            player.Deck.Hand.MoveCardsTo(player.Deck.DiscardPile, 3);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5,2,3,0)));

            player.Draw(5);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(0, 7, 3, 0)));

            player.ChooseAndDiscard(3);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(0, 4, 6, 0)));

            player.Draw(3);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(3, 7, 0, 0)));

        }


        [Test, ExpectedException(typeof(NotEnoughCardsException))]
        public void ExceptionWhenDrawFromEmptyDeck()
        {
            IoC.Kernel.ReBind<IDeck>().To<StartingDeck>();

            var player = IoC.Kernel.Get<Player>();
            player.Ready();

            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5, 5, 0, 0)));

            player.Draw(5);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(0, 10, 0, 0)));

            player.Draw(2);

        }


        [Test]
        public void CardGet()
        {
            EmptyCard.ResetCounters();
            IoC.Kernel.Rebind<ICardShuffler>().To<CardShuffleBySorting>();

            var player = IoC.Kernel.Get<Player>();
            // player.Ready();

            var sample = player.Deck.Library.Get(1, Position.Top).ToList();

            Assert.That(sample.Count(), Is.EqualTo(1));
            Assert.That(((EmptyCard)sample.First()).Index, Is.EqualTo(0));
            Assert.That(((EmptyCard)sample.Last()).Index, Is.EqualTo(0));

            sample = player.Deck.Library.Get(5, Position.Top).ToList();

            Assert.That(sample.Count(), Is.EqualTo(5));
            Assert.That(((EmptyCard)sample.First()).Index, Is.EqualTo(0));
            Assert.That(((EmptyCard)sample.Last()).Index, Is.EqualTo(4));

            sample = player.Deck.Library.Get(1, Position.Bottom).ToList();

            Assert.That(sample.Count(), Is.EqualTo(1));
            Assert.That(((EmptyCard)sample.First()).Index, Is.EqualTo(9));
            Assert.That(((EmptyCard)sample.Last()).Index, Is.EqualTo(9));

            sample = player.Deck.Library.Get(4, Position.Bottom).ToList();

            Assert.That(sample.Count(), Is.EqualTo(4));
            Assert.That(((EmptyCard)sample.First()).Index, Is.EqualTo(6));
            Assert.That(((EmptyCard)sample.Last()).Index, Is.EqualTo(9));
        }


  

        [Test, ExpectedException(typeof(NotEnoughCardsException))]
        public void GetTooManyCardsFromZone()
        {
            IoC.Kernel.ReBind<IDeck>().To<TestDeck>();

            var deck = IoC.Kernel.Get<IDeck>();
            deck.Ready();

            Assert.That(deck.Cards.Count, Is.EqualTo(10));

            deck.Library.Get(11);

        }

        [ExpectedException(typeof (NotSupportedException))]
        [Test]
        public void CardsInDeckAreReadOnly()
        {
            var deck = IoC.Kernel.Get<IDeck>();

            deck.Cards.Add(new EmptyCard());
        }



        [TestCase(4, PlayerChoice.Left)]
        [TestCase(4, PlayerChoice.Right)]
        [TestCase(4, PlayerChoice.Current)]
        public void PlayersSitOnARoundTable(int numberOfPlayers, PlayerChoice toBeLocated)
        {
            IoC.Kernel.Unbind<IPlayer>();
            IoC.Kernel.BindMultipleTimesTo<IPlayer, Player>(numberOfPlayers);
            
            var game = IoC.Kernel.Get<IGame>();
            var locatedPlayer = game.GetPlayers(toBeLocated).Single();
            int expectedIndex;

            switch (toBeLocated)
            {
                case PlayerChoice.Left:
                    expectedIndex = numberOfPlayers - 1;
                    break;
                case PlayerChoice.Right:
                    expectedIndex = 1;
                    break;
                case PlayerChoice.Current:
                    expectedIndex = 0;
                    break;
                default:
                    throw new NotImplementedException();
            }

            Assert.That(locatedPlayer, Is.EqualTo(game.Players[expectedIndex]));

        }


        [Test]
        public void DeckReady()
        {
            var deck = IoC.Kernel.Get<IDeck>();

            Assert.That(deck.CardCountByZone, Is.EqualTo(new CardRepartition(
                                                            library:10, 
                                                            hand: 0, 
                                                            discard: 0, 
                                                            battlefield: 0)));

            deck.Ready();
            Assert.That(deck.CardCountByZone, Is.EqualTo(new CardRepartition(
                                                            library: 10,
                                                            hand: 0,
                                                            discard: 0,
                                                            battlefield: 0)));

            Assert.That(deck.Hand.Cards, Has.All.Matches<ICard>(c => c.Zone == deck.Hand));
            Assert.That(deck.Library.Cards, Has.All.Matches<ICard>(c => c.Zone == deck.Library));

        }

        [Test]
        public void GameReady()
        {
            var game = IoC.Kernel.Get<IGame>();
            var deck = game.Players[0].Deck;

            Assert.That(deck.CardCountByZone, Is.EqualTo(new CardRepartition(
                                                            library: 10,
                                                            hand: 0,
                                                            discard: 0,
                                                            battlefield: 0)));

            deck.Ready();
            Assert.That(deck.CardCountByZone, Is.EqualTo(new CardRepartition(
                                                            library: 10,
                                                            hand: 0,
                                                            discard: 0,
                                                            battlefield: 0)));

            game.Ready();
            Assert.That(deck.CardCountByZone, Is.EqualTo(new CardRepartition(
                                                            library: 5,
                                                            hand: 5,
                                                            discard: 0,
                                                            battlefield: 0)));
        }

        [TestCase(1)]
        [TestCase(4)]
        [TestCase(30)]
        public void GameReadyAffectsAllPlayers(int nbPlayers)
        {
            IoC.Kernel.Unbind<IPlayer>();
            IoC.Kernel.BindMultipleTimesTo<IPlayer, Player>(nbPlayers);

            var game = IoC.Kernel.Get<IGame>();

            foreach (var player in game.Players)
            {
                Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(10,0,0,0)));
            }

            game.Ready();

            foreach (var player in game.Players)
            {
                Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5,5,0,0)));
            }

            Assert.That(game.CurrentPlayer, Is.EqualTo(game.Players[0]));

        }

        [Test]
        public void DeckGetZone()
        {
            var deck = IoC.Kernel.Get<IDeck>();

            Assert.That(deck.Get(ZoneChoice.Discard), Is.EqualTo(deck.DiscardPile));
            Assert.That(deck.Get(ZoneChoice.Hand), Is.EqualTo(deck.Hand));
            Assert.That(deck.Get(ZoneChoice.Library), Is.EqualTo(deck.Library));
            Assert.That(deck.Get(ZoneChoice.Play), Is.EqualTo(deck.BattleField));
        }


        [Test]
        public void ToStringTests()
        {
            // Not much to test here. Just do some coverage fill
            IoC.Kernel.Bind<ICard>().To<BindableCard>();

            var game = IoC.Kernel.Get<IGame>();
            game.Ready();

            game.ToString();

            foreach (var player in game.Players)
            {
                player.ToString();
                player.AvailableResources.ToString();
                player.Deck.ToString();
                player.Deck.Hand.ToString();
                player.Deck.Library.ToString();
                player.Deck.DiscardPile.ToString();
                player.Deck.BattleField.ToString();
                player.Deck.Cards.First().ToString();
            }
        }


        [Test]
        public void LibraryKnowsParentDeck()
        {
            var deck = IoC.Kernel.Get<IDeck>();
            deck.Ready();

            Assert.That(deck.Library.ParentDeck, Is.EqualTo(deck));
        }

        [Test]
        public void LibraryKnowsParentDeckInGame()
        {
            var game = IoC.Kernel.Get<IGame>();
            game.Ready();

            Assert.That(game.CurrentPlayer.Deck.Library.ParentDeck, Is.EqualTo(game.CurrentPlayer.Deck));
        }

        [TestCase(0, 0, 0, 0)]
        [TestCase(1, 0, 0, 0)]
        [TestCase(1, 0, 1, 0)]
        [TestCase(0, 1, 0, 0)]
        [TestCase(0, 1, 0, 1)]
        [TestCase(5, 1, 5, 1)]
        [TestCase(5, 4, 5, 4)]
        [TestCase(5, 4, 0, 0)]
        public void ResourcesPay(int haveCoins, int havePot, int needCoins, int needPot)
        {
            var available = new Resources(haveCoins, havePot);
            var price = new Resources(needCoins, needPot);

            available.Pay(price);

            Assert.That(available, Is.EqualTo(new Resources(haveCoins - needCoins, havePot - needPot)));

        }

        [ExpectedException(typeof (InsufficientResourcesException))]
        [TestCase(0, 0, 1, 0)]
        [TestCase(0, 0, 0, 1)]
        [TestCase(1, 0, 1, 1)]
        [TestCase(0, 1, 1, 0)]
        [TestCase(5, 1, 5, 2)]
        [TestCase(5, 1, 6, 1)]
        [TestCase(5, 4, 4, 5)]
        public void ResourcesPayRobustness(int haveCoins, int havePot, int needCoins, int needPot)
        {
            var available = new Resources(haveCoins, havePot);
            var price = new Resources(needCoins, needPot);

            available.Pay(price);

            Assert.That(available, Is.EqualTo(new Resources(haveCoins - needCoins, havePot - needPot)));

        }


        [ExpectedException(typeof (InvalidOperationException))]
        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        public void ResourcesRobustness(int coins, int potions)
        {
            var resources = new Resources(coins, potions);
        }

        [TestCase(0, 1)]
        [TestCase(10, 1)]
        [TestCase(10, 0)]
        [TestCase(1, 0)]
        public void ResourcesReset(int coins, int potions)
        {
            var resources = new Resources(coins, potions);
            Assert.That(resources, Is.EqualTo(new Resources(coins, potions)));

            resources.Reset();
            Assert.That(resources, Is.EqualTo(new Resources(0, 0)));
        }



        [TestCase(0, 0, 0, 0, true)]
        [TestCase(0, 1, 0, 1, true)]
        [TestCase(1, 0, 1, 0, true)]
        [TestCase(1, 0, 0, 1, false)]
        [TestCase(5, 4, 4, 5, false)]
        [TestCase(5, 4, 5, 4, true)]
        public void ResourcesEquals(int aCoins, int aPot, int bCoins, int bPot, bool expected)
        {
            var a = new Resources(aCoins, aPot);
            var b = new Resources(bCoins, bPot);

            Assert.That(a.Equals(b), Is.EqualTo(expected));
        }


    }
}
