using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using NUnit.Framework;
using org.gbd.Dominion.Model;
using org.gbd.Dominion.Model.GameMechanics;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.Test
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
            Game.MakeReadyToStart();

        }


        [Test]
        public void SmokeTest()
        {
            
        }

        [Test]
        public void PlayerRecievesACard()
        {
            Game.CurrentPlayer.Receive(Game.SupplyZone.Cards.First());
        }

        [Test]
        public void PlayerBuysACard()
        {
            Game.CurrentPlayer.LastTurnStatus.Coins = 100;
            Game.CurrentPlayer.Buy(Game.SupplyZone.Cards.First());

            Assert.That(Game.CurrentPlayer.Deck.Cards.Count == 11);
        }


        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlayerBuysAnIllegalCard()
        {
            Game.CurrentPlayer.LastTurnStatus.Coins = 100;
            Game.CurrentPlayer.Buy(Game.SupplyZone.Cards.First());

            Assert.Fail();
        }


        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlayerBuysAnInvalidCard()
        {
            Game.CurrentPlayer.LastTurnStatus.Coins = 100;
            Game.CurrentPlayer.Buy(IoC.Kernel.Get<ICard>());

            Assert.Fail();
        }

        [Test]
        public void DummyTest()
        {
            throw new NotImplementedException();
        }



    }
}
