using System;
using System.Linq;
using System.Reflection;
using gbd.Dominion.Model;
using gbd.Dominion.Model.Cards;
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
    public class GameScenarioTest: BaseTest
    {
        [TestFixtureSetUp]
        public new void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
        }

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
            IoC.Kernel.Get<IGame>().CurrentPlayer.Status.Resources = new Resources(100);
            IoC.Kernel.Get<IGame>().CurrentPlayer.Buy(IoC.Kernel.Get<IGame>().SupplyZone.Cards.First());

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


    }
}
