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
            Game.G.Ready();

        }


        [Test]
        public void SmokeTest()
        {
            
        }

        [Test]
        public void PlayerRecievesACard()
        {
            Game.G.CurrentPlayer.Receive(Game.G.SupplyZone.Cards.First());
        }

        [Test]
        public void PlayerBuysACard()
        {
            Game.G.CurrentPlayer.Status.Resources = new Resources(100);
            Game.G.CurrentPlayer.Buy(Game.G.SupplyZone.Cards.First());

            Assert.That(Game.G.CurrentPlayer.Deck.Cards.Count == 11);
        }


        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlayerBuysAnIllegalCard()
        {
            Game.G.CurrentPlayer.Status.Resources = new Resources(100);
            Game.G.CurrentPlayer.Buy(Game.G.SupplyZone.Cards.First());

            Assert.Fail();
        }


        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlayerBuysAnInvalidCard()
        {
            Game.G.CurrentPlayer.Status.Resources = new Resources(100);
            Game.G.CurrentPlayer.Buy(IoC.Kernel.Get<ICard>());

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
