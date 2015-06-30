using System;
using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
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




        [Test]
        public void Curse()
        {
            var card = IoC.Kernel.Get<Curse>();

            Assert.That(card.Mechanics.Cost, Is.EqualTo(new Resources(0)));
            Assert.That(card.Mechanics.VictoryPoints, Is.EqualTo(-1));
            Assert.That(card.Mechanics.TreasureValue, Is.EqualTo(new Resources(0)));
       }
    }
}
 





