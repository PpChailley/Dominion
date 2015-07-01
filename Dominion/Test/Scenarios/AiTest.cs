using System;
using System.Linq;
using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Injection;
using gbd.Dominion.Model;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.AI;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
using gbd.Tools.NInject;
using Ninject;
using NUnit.Framework;

namespace gbd.Dominion.Test.Scenarios
{
    [TestFixture]
    public class AiTest : BaseTest
    {

        [Test]
        public void EnoughAiImplemented()
        {
            Assert.That(ReflectionClassFinder.GetAllAiTestCaseData().Count(), Is.GreaterThan(0));
        }

 
        [Test, TestCaseSource(typeof(ReflectionClassFinder), "GetAllAiTestCaseData")]
        public void AiDiscardUseCase(Type ai)
        {
            IoC.Kernel.ReBind<IDeck>().To<TestDeck>();
            IoC.Kernel.ReBind<IAi>().To(ai);

            var player = IoC.Kernel.Get<Player>();
            player.Ready();
            player.StartTurn();
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5,5,0,0)));

            player.I.Discard(0);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5, 5, 0, 0)));

            player.I.Discard(1);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5, 4, 1, 0)));

            player.I.Discard(2);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5, 2, 3, 0)));

            player.Draw(1);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(4, 3, 3, 0)));

            player.I.Discard(2);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(4, 1, 5, 0)));
        }

        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(0, 5)]
        [TestCase(1, 5)]
        [TestCase(1, 6)]
        [TestCase(5, 10)]
        public void Discard(int drawAmount, int discardAmount)
        {
            var player = IoC.Kernel.Get<IPlayer>();
            player.Ready();
            player.StartTurn();

            player.Draw(drawAmount);
            var discarded = player.I.Discard(discardAmount);

            Assert.That(discarded, Has.Count.EqualTo(discardAmount));
            Assert.That(discarded, Has.All.Matches<ICard>(c => c.Zone == player.Deck.DiscardPile));
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(
                                            library: 5 - drawAmount, 
                                            hand: 5 + drawAmount - discardAmount, 
                                            discard: discardAmount, 
                                            battlefield: 0  )));
        }


        [ExpectedException(typeof (NotEnoughCardsException))]
        [TestCase(0, 6)]
        [TestCase(0, 99)]
        [TestCase(1, 7)]
        [TestCase(5, 11)]
        public void DiscardRobustness(int drawAmount, int discardAmount)
        {
            var player = IoC.Kernel.Get<IPlayer>();
            player.Ready();
            player.StartTurn();
            player.Draw(drawAmount);

            player.I.Discard(discardAmount);
        }


        [Test]
        public void Receive()
        {
            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.BindTo<ICard, Copper>(10).WhenAnyAncestorOfType<Copper, ILibrary>();

            var game = IoC.Kernel.Get<IGame>();
            game.Ready();
            var player = game.CurrentPlayer;


        }
 



    }


}