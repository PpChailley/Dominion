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


        [TestCase(typeof(Copper))]
        public void SupplyHasRequiredPiles(Type alwaysAvailableCardType)
        {
            var supply = IoC.Kernel.Get<ISupplyZone>();

            Assert.That(supply.PileOf(alwaysAvailableCardType), Has.Count.EqualTo(1));
        }

    }
}
