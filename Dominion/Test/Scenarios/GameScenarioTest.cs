using System;
using System.Linq;
using gbd.Dominion.Model;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
using gbd.Dominion.Tools;
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

            IoC.ReBind<IDeck>().To<EasyToTrackDeck>();
            Game.G.MakeReadyToStart();

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
        public void DummyTest()
        {
            throw new NotImplementedException();
        }



    }
}
