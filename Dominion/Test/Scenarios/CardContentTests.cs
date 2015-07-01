using System;
using gbd.Dominion.Contents;
using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Injection;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
using gbd.Tools.NInject;
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

        [TestCase(typeof(Estate), 2, 1)]
        [TestCase(typeof(Duchy), 5, 3)]
        [TestCase(typeof(Province), 8, 6)]
        public void CheckBasicVictoryProperties(Type cardType, int cost, int value)
        {
            IoC.Kernel.Bind<ICard>().To(cardType);

            var card = IoC.Kernel.Get<ICard>();

            Assert.That(card.Mechanics.Cost, Is.EqualTo(new Resources(cost)));
            Assert.That(card.Mechanics.VictoryPoints, Is.EqualTo(value));
            Assert.That(card.Mechanics.TreasureValue, Is.EqualTo(new Resources(0)));
        }


        [Test]
        public void Curse()
        {
            var card = IoC.Kernel.Get<Curse>();

            Assert.That(card.Mechanics.Cost, Is.EqualTo(new Resources(0)));
            Assert.That(card.Mechanics.VictoryPoints, Is.EqualTo(-1));
            Assert.That(card.Mechanics.TreasureValue, Is.EqualTo(new Resources(0)));
       }


        [TestCase(10, 1, 1)]
        [TestCase(10, 2, 2)]
        [TestCase(10, 9, 9)]
        [TestCase(10, 10, 20)]
        [TestCase(18, 1, 1)]
        [TestCase(19, 1, 2)]
        public void Gardens(int coppers, int gardens, int expectedScore)
        {
            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.BindMultipleTimesTo<ICard, Copper>(coppers).WhenAnyAncestorOfType<Copper, ILibrary>();
            IoC.Kernel.BindMultipleTimesTo<ICard, Gardens>(gardens).WhenAnyAncestorOfType<Gardens, ILibrary>();


            var deck = IoC.Kernel.Get<IDeck>();
            deck.Ready();

            Assert.That(deck.Score, Is.EqualTo(expectedScore));
        }




    }
}
 





