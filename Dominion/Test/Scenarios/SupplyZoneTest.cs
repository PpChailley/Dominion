using System;
using System.Linq;
using System.Reflection;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Tools;
using Ninject;
using NUnit.Framework;

namespace gbd.Dominion.Test.Scenarios
{
    [TestFixture]
    class SupplyZoneTest
    {

        [Test]
        public void SupplyZone()
        {
            throw new NotImplementedException();
        }


        [Test]
        public void SupplyPile()
        {
            IoC.ReBind<ISupplyPile>().To<SupplyPile>();


            var pile = IoC.Kernel.Get<ISupplyPile>();
            var player = IoC.Kernel.Get<IPlayer>();

            Game.MoveCards(pile, player.DiscardPile);

            Assert.That(pile.Cards.Count, Is.EqualTo(9));
            Assert.That(player.Deck.Cards.Count, Is.EqualTo(11));

        }

        [Test]
        public void EnoughCardsForPlayableSupplyZone()
        {
            var classes = Assembly.GetExecutingAssembly().GetTypes();

            var cards = classes.Where(t => typeof(SelectableCard).IsAssignableFrom(t)
                                                               && t.IsInterface == false
                                                               && t.IsAbstract == false);


            Assert.That(cards.Count(), Is.GreaterThanOrEqualTo(10));
        }

        




    }
}
