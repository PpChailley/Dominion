using System;
using System.Linq;
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
    public class PlayerTest: BaseTest
    {

        [Test]
        public void StartTurn()
        {
            var player = IoC.Kernel.Get<IPlayer>();
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(10, 0, 0, 0)));

            player.Ready();
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5, 5, 0, 0)));

            player.StartTurn();
            Assert.That(player.AvailableResources, Is.EqualTo(new Resources(0)));
            Assert.That(player.AvailableBuys, Is.EqualTo(1));
            Assert.That(player.AvailableActions, Is.EqualTo(1));

        }

        [Test]
        public void EndTurn()
        {
            var player = IoC.Kernel.Get<IPlayer>();
            player.Ready();
            player.StartTurn();
            Assert.That(player.AvailableResources, Is.EqualTo(new Resources(0)));
            Assert.That(player.AvailableBuys, Is.EqualTo(1));
            Assert.That(player.AvailableActions, Is.EqualTo(1));

            player.AvailableActions = 12;
            player.AvailableBuys = 12;
            player.AvailableResources = new Resources(5,48);
            player.Draw(1);

            player.EndTurn();
            Assert.That(player.AvailableResources, Is.EqualTo(new Resources(0)));
            Assert.That(player.AvailableBuys, Is.EqualTo(0));
            Assert.That(player.AvailableActions, Is.EqualTo(0));
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5,5,0,0)));

        }



        [Test]
        public void Receive()
        {
            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.BindMultipleTimesTo<ICard, BindableCard>(10).WhenAnyAncestorOfType<BindableCard, ILibrary>();
            IoC.Kernel.BindMultipleTimesTo<ICard, BindableCard>(10).WhenAnyAncestorOfType<BindableCard, ISupplyZone>();

            var game = IoC.Kernel.Get<IGame>();
            game.Ready();

            var card = game.SupplyZone.Cards.First();

            game.CurrentPlayer.Receive(card);

            Assert.That(game.CurrentPlayer.Deck.DiscardPile.Cards, Contains.Item(card));
            Assert.That(card.Zone, Is.EqualTo(game.CurrentPlayer.Deck.DiscardPile));

        }

    


    }
}
