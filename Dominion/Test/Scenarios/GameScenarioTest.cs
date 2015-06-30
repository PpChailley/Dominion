using System;
using System.Linq;
using System.Reflection;
using gbd.Dominion.Model;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.AI;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
using gbd.Dominion.Tools;
using gbd.Tools.NInject;
using Ninject;
using NUnit.Framework;

namespace gbd.Dominion.Test.Scenarios
{

    [TestFixture]
    public class GameScenarioTest: BaseTest
    {
       

        [SetUp]
        public new void SetUp()
        {
            base.SetUp();

            IoC.Kernel.ReBind<IDeck>().To<TestDeck>();
            IoC.Kernel.Get<IGame>().Ready();

        }


        [Test]
        public void SmokeTest()
        {
            
        }

        [Test]
        public void PlayerRecievesACard()
        {
            var supply = IoC.Kernel.Get<IGame>().SupplyZone;
            IoC.Kernel.Get<IGame>().CurrentPlayer.Receive(supply.Cards.First());
            throw new NotImplementedException();
        }

        [Test]
        public void PlayerBuysACard()
        {
            var player = IoC.Kernel.Get<IGame>().CurrentPlayer;
            player.Status.Resources = new Resources(100);
            player.Buy(IoC.Kernel.Get<IGame>().SupplyZone.Cards.First());

            Assert.That(IoC.Kernel.Get<IGame>().CurrentPlayer.Deck.Cards.Count == 11);
        }


        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlayerBuysAnIllegalCard()
        {
            IoC.Kernel.Get<IGame>().CurrentPlayer.Status.Resources = new Resources(100);
            IoC.Kernel.Get<IGame>().CurrentPlayer.Buy(IoC.Kernel.Get<IGame>().SupplyZone.Cards.First());

            Assert.Fail();
        }


        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlayerBuysAnInvalidCard()
        {
            IoC.Kernel.Get<IGame>().CurrentPlayer.Status.Resources = new Resources(100);
            IoC.Kernel.Get<IGame>().CurrentPlayer.Buy(IoC.Kernel.Get<ICard>());

            Assert.Fail();
        }



        [Test]
        public void EnoughCardsImplementedForPlayableSupplyZone()
        {
            var classes = Assembly.GetExecutingAssembly().GetTypes();

            var cards = classes.Where(t => typeof(SelectableCard).IsAssignableFrom(t)
                                                               && t.IsInterface == false
                                                               && t.IsAbstract == false);


            Assert.That(cards.Count(), Is.GreaterThanOrEqualTo(10));
        }


        [Test, TestCaseSource(typeof(ReflectionClassFinder), "GetAllAiTestCaseData")]
        public void AiHandleMultipleDiscards(Type ai)
        {
            IoC.Kernel.ReBind<IDeck>().To<TestDeck>();
            IoC.Kernel.ReBind<IAi>().To(ai);

            var player = IoC.Kernel.Get<Player>();
            player.Ready();
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5, 5, 0, 0)));

            player.ChooseAndDiscard(0);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5, 5, 0, 0)));

            player.ChooseAndDiscard(1);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5, 4, 1, 0)));

            player.ChooseAndDiscard(2);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5, 2, 3, 0)));

            player.Draw(1);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(4, 3, 3, 0)));

            player.ChooseAndDiscard(2);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(4, 1, 5, 0)));

            player.Draw(6);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(3, 7, 0, 0)));

            player.ChooseAndDiscard(5);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(3, 2, 5, 0)));

            player.Draw(1);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(2, 3, 5, 0)));
        }

    }
}
