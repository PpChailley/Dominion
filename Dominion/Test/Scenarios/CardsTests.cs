using System;
using System.Linq;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
using gbd.Dominion.Tools;
using gbd.Tools.NInject;
using Ninject;
using NUnit.Framework;

namespace gbd.Dominion.Test.Scenarios
{
    [TestFixture]
    public  class CardsTests: BaseTest
    {



        [Test]
        public void CardsFollowTheirZone()
        {
            IoC.Kernel.Unbind<ISupplyPile>();
            IoC.Kernel.Bind<ISupplyPile>().To<SupplyPile>();
            
            var supplyPile = IoC.Kernel.Get<ISupplyPile>();
            var deck = IoC.Kernel.Get<IDeck>();
            supplyPile.Ready();
            deck.Ready();
            

            Assert.That(supplyPile.Cards.First().Zone, Is.EqualTo(supplyPile));
            Assert.That(deck.Cards.First().Zone, Is.EqualTo(deck.Library));

            var movingCard = supplyPile.Cards.First();

            movingCard.MoveTo(deck.Library);

            Assert.That(movingCard.Zone, Is.EqualTo(deck.Library));
        }

        [TestCase(0, 0, Position.Top)]
        [TestCase(0, 0, Position.Bottom)]
        [TestCase(10, 0, Position.Top)]
        [TestCase(10, 0, Position.Bottom)]
        [TestCase(0, 10, Position.Top)]
        [TestCase(0, 10, Position.Bottom)]
        [TestCase(555, 222, Position.Top)]
        [TestCase(555, 222, Position.Bottom)]
        public void MovePileToPile_1by1(int sourceSize, int destSize, Position position)
        {
            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.Unbind<ISupplyPile>();
            IoC.Kernel.BindMultipleTimes<ICard>(sourceSize).To<ICard, TestCard>();
            IoC.Kernel.Bind<ISupplyPile>().To<SupplyPile>();
            var src = IoC.Kernel.Get<ISupplyPile>();

            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.BindMultipleTimes<ICard>(destSize).To<ICard, TestCard>();
            var dst = IoC.Kernel.Get<ISupplyPile>();

            src.Ready();
            dst.Ready();

            for (int i = 0; i < sourceSize; i++)
            {
                var card = src.Cards.First();
                Assert.That(card.Zone, Is.EqualTo(src));
                
                card.MoveTo(dst);

                Assert.That(src.Cards.Count, Is.EqualTo(sourceSize - i - 1));
                Assert.That(dst.Cards.Count, Is.EqualTo(destSize + i + 1));
                Assert.That(card.Zone, Is.EqualTo(dst));
            }

            Assert.That(src.Cards.Count, Is.EqualTo(0));
            Assert.That(dst.Cards.Count, Is.EqualTo(destSize + sourceSize));
            


        }


        [Test]
        public void MoveCards()
        {
            IoC.Kernel.Unbind<ISupplyPile>();
            IoC.Kernel.Bind<ISupplyPile>().To<SupplyPile>();
            
            var pileA = IoC.Kernel.Get<ISupplyPile>();
            var pileB = IoC.Kernel.Get<ISupplyPile>();

            pileA.Ready();

            var card = pileA.Cards.First();
            Assert.That(card.Zone, Is.EqualTo(pileA));

            card.MoveTo(pileB);
            Assert.That(card.Zone, Is.EqualTo(pileB));

        }

    }
}
