using System;
using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Model.Cards;
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
            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.BindMultipleTimesTo<ICard, EmptyCard>(10).WhenAnyAncestorOfType<EmptyCard, ISupplyZone>();
            IoC.Kernel.BindMultipleTimesTo<ICard, BindableCard>(10).WhenAnyAncestorOfType<BindableCard, ISupplyZone>();

            var supply = IoC.Kernel.Get<ISupplyZone>();

            Assert.That(supply.Piles.Count, Is.EqualTo(2));
            Assert.That(supply.Cards.Count, Is.EqualTo(20));
        }


        [Test]
        public void SupplyPile()
        {
            IoC.Kernel.Unbind<ISupplyPile>();
            IoC.Kernel.Bind<ISupplyPile>().To<SupplyPile>();

            var pile = IoC.Kernel.Get<ISupplyPile>();
            var deck = IoC.Kernel.Get<IDeck>();

            Assert.That(deck.Cards.Count, Is.EqualTo(10));
            Assert.That(pile.Cards.Count, Is.EqualTo(10));
        }



        [Test]
        public void MoveCardFromSupply()
        {
            IoC.Kernel.Unbind<ISupplyPile>();
            IoC.Kernel.Bind<ISupplyPile>().To<SupplyPile>();

            var pile = IoC.Kernel.Get<ISupplyPile>();
            var player = IoC.Kernel.Get<IPlayer>();

            pile.Ready();
            player.Ready();

            Assert.That(player.Deck.Cards.Count, Is.EqualTo(10));
            Assert.That(pile.Cards.Count, Is.EqualTo(10));


            pile.MoveCardsTo(player.Deck.Library, 1);

            Assert.That(player.Deck.Cards.Count, Is.EqualTo(11));
            Assert.That(pile.Cards.Count, Is.EqualTo(9));
        }



    }
}
