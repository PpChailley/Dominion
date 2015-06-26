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
    public class InjectionOfMechanicsTests : BaseTest
    {

        [Test]
        public void InjectionOfMechanics()
        {
            IoC.Kernel.Unbind<IGameAction>();
            IoC.Kernel.Bind<IGameAction>().ToConstructor(x => new Draw(1));
            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.BindMultipleTimesTo<ICard, BindableCard>(10).WhenAnyAncestorOfType<BindableCard, ILibrary>();

            var witnessPlayer = IoC.Kernel.Get<IPlayer>();


            var game = IoC.Kernel.Get<IGame>();
            game.Ready();
            var player = game.CurrentPlayer;


            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5, 5, 0, 0)));

            player.Play(player.Deck.Hand.Cards.First());

            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(4, 5, 0, 1)));
        }


    }
}
