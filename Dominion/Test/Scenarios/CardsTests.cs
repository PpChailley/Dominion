using System;
using System.Linq;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
using gbd.Dominion.Tools;
using Ninject;
using NUnit.Framework;

namespace gbd.Dominion.Test.Scenarios
{
    [TestFixture]
    public  class CardsTests: BaseTest
    {

        [Test]
        public void InjectionOfMechanics()
        {
            IoC.Kernel.Unbind<IGameAction>();
            IoC.Kernel.Bind<IGameAction>().To<Draw>();

            var witnessPlayer = IoC.Kernel.Get<IPlayer>();


            var game = IoC.Kernel.Get<IGame>();
            game.Ready();

            var player = game.CurrentPlayer;

            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5,5,0,0)));

            player.Play(player.Deck.Hand.Cards.First());

            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(4,5,0,1)));
        }

        [Test]
        public void CardsFollowTheirZone()
        {
            var supplyPile = IoC.Kernel.Get<ISupplyPile>();
            var deck = IoC.Kernel.Get<IDeck>();
            

            Assert.That(supplyPile.Cards.First().Zone, Is.EqualTo(supplyPile));
            Assert.That(deck.Cards.First().Zone, Is.EqualTo(deck));

            var movingCard = supplyPile.Cards.First();

            Model.GameMechanics.Model.MoveCard(movingCard, supplyPile, deck);

            Assert.That(movingCard.Zone, Is.EqualTo(deck));
        }

    }
}
