<<<<<<< .mine
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gbd.Dominion.Contents.Cards;
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gbd.Dominion.Contents.Cards;
>>>>>>> .theirs
using gbd.Dominion.Model.Cards;
<<<<<<< .mine
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


        [TestCase(typeof(Copper), 0, 1)]
        [TestCase(typeof(Silver), 3, 2)]
        [TestCase(typeof(Gold), 6, 3)]
        public void CheckBasicTreasureProperties(Type cardType, int cost, int value)
        {
            IoC.Kernel.Bind<ICard>().To(cardType);

            var card = IoC.Kernel.Get<ICard>();

            Assert.That(card.Mechanics.Cost, Is.EqualTo(new Resources(cost)));
            Assert.That(card.Mechanics.VictoryPoints, Is.EqualTo(0));
            Assert.That(card.Mechanics.TreasureValue, Is.EqualTo(new Resources(value)));

        }

    }
}
=======
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


        [TestCase(typeof(Copper), 0, 1)]
        [TestCase(typeof(Silver), 3, 2)]
        [TestCase(typeof(Gold), 6, 3)]
        public void CheckBasicTreasureProperties(Type cardType, int cost, int value)
        {
            IoC.Kernel.Bind<ICard>().To(cardType);

            var card = IoC.Kernel.Get<ICard>();

            Assert.That(card.Mechanics.Cost, Is.EqualTo(new Resources(cost)));
            Assert.That(card.Mechanics.VictoryPoints, Is.EqualTo(0));
            Assert.That(card.Mechanics.TreasureValue, Is.EqualTo(new Resources(value)));

        }
    }
}

>>>>>>> .theirs
