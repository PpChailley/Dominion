using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
using gbd.Dominion.Tools;
using Ninject;
using NUnit.Framework;

namespace gbd.Dominion.Test.Scenarios
{
    [TestFixture]
    class StandardGameTests: BaseTest
    {

        [SetUp]
        public new void SetUp()
        {
            IoC.Kernel = new StandardKernel(    new IoCMechanicsModule(),
                                                new IoCCardsModule(),
                                                new IoCStandardGameModule());
        }


        [TestCase(typeof(Copper), IoCStandardGameModule.NB_COPPER)]
        [TestCase(typeof(Silver), IoCStandardGameModule.NB_SILVER)]
        [TestCase(typeof(Gold), IoCStandardGameModule.NB_GOLD)]
        [TestCase(typeof(Estate), IoCStandardGameModule.NB_ESTATE)]
        [TestCase(typeof(Duchy), IoCStandardGameModule.NB_DUCHY)]
        [TestCase(typeof(Province), IoCStandardGameModule.NB_PROVINCE)]
        [TestCase(typeof(Curse), IoCStandardGameModule.NB_CURSE)]
        public void SupplyHasRequiredPiles(Type t, int expectedCount)
        {
            var supply = IoC.Kernel.Get<ISupplyZone>();

            Assert.That(supply.PileOf(t).Cards, Has.Count.EqualTo(expectedCount));
        }


        [Test]
        public void StandardDeck()
        {
            var deck = IoC.Kernel.Get<IDeck>();

            Assert.That(deck.Cards.Count(c => c.GetType() == typeof(Copper)), Is.EqualTo(7));
            Assert.That(deck.Cards.Count(c => c.GetType() == typeof(Estate)), Is.EqualTo(3));
        }

    }
}
