using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
using gbd.Dominion.Tools;
using Ninject;
using NUnit.Framework;

namespace gbd.Dominion.Test.Scenarios
{

    [TestFixture]
    class CardContentTests: BaseTest
    {

        [SetUp]
        public new void SetUp()
        {
            base.SetUp();
            IoC.Kernel.Unbind<ICard>();
        }


        [Test]
        public void Copper()
        {
            IoC.Kernel.Bind<ICard>().To<Copper>();

            var card = IoC.Kernel.Get<ICard>();

            Assert.That(card.Mechanics.Cost, Is.EqualTo(new Resources(0)));
            Assert.That(card.Mechanics.VictoryPoints, Is.EqualTo(0));
            Assert.That(card.Mechanics.TreasureValue, Is.EqualTo(new Resources(1)));

        }
    }
}
