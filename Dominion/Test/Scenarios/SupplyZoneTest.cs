using System.Collections.Generic;
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
    public class SupplyZoneTest: BaseTest
    {


        [Test]
        public void SupplyZone()
        {
            //IoC.Kernel.ReBind<ICollection<IPlayer>>()
            //    .ToConstructor(x => new List<IPlayer>(x.Inject<IList<IPlayer>>()));


            var player = IoC.Kernel.Get<IPlayer>();
            var supply = IoC.Kernel.Get<ISupplyZone>();

            Assert.That(player.Deck.Cards.Count, Is.EqualTo(10));
            Assert.That(supply.Piles.Count, Is.EqualTo(10));
            Assert.That(supply.Cards.Count, Is.EqualTo(100));

        }


        [Test]
        public void SupplyPile()
        {
            IoC.Kernel.Unbind<ISupplyPile>();
            IoC.Kernel.Bind<ISupplyPile>().To<TestSupplyPile>();

            var pile = IoC.Kernel.Get<ISupplyPile>();
            var deck = IoC.Kernel.Get<IDeck>();

            Assert.That(deck.Cards.Count, Is.EqualTo(10));
            Assert.That(pile.Cards.Count, Is.EqualTo(10));
        }



        [Test]
        public void MoveCardFromSupply()
        {
            IoC.Kernel.Unbind<ISupplyPile>();
            IoC.Kernel.Bind<ISupplyPile>().To<TestSupplyPile>();

            var pile = IoC.Kernel.Get<ISupplyPile>();
            var player = IoC.Kernel.Get<IPlayer>();

            Assert.That(player.Deck.Cards.Count, Is.EqualTo(10));
            Assert.That(pile.Cards.Count, Is.EqualTo(10));


            Model.GameMechanics.Model.MoveCards(pile, player.Deck.DiscardPile);

            Assert.That(player.Deck.Cards.Count, Is.EqualTo(11));
            Assert.That(pile.Cards.Count, Is.EqualTo(9));
        }



        




    }
}
