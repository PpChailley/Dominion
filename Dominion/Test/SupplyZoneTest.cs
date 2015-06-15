using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using NUnit.Framework;
using org.gbd.Dominion.Model;
using org.gbd.Dominion.Model.Cards;
using org.gbd.Dominion.Model.GameMechanics;
using org.gbd.Dominion.Model.Zones;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.Test
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
