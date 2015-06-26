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
    public class GameActionTests: BaseTest
    {

        [SetUp]
        public new void SetUp()
        {
            base.SetUp();

            IoC.Kernel.Unbind<ICard>();
            IoC.Kernel.Unbind<IGameAction>();
            IoC.Kernel.Unbind<ICardType>();
            IoC.Kernel.Unbind<IPlayer>();

            IoC.Kernel.Bind<IPlayer>().To<Player>().InSingletonScope();

        }

 
        
        [TestCase(10, 1)]
        [TestCase(10, 2)]
        [TestCase(10, 5)]
        [TestCase(10, 1)]
        public void Draw(int deckSize, int drawAmount)
        {
            IoC.Kernel.BindMultipleTimes<ICard>(deckSize).To<ICard, BindableCard>().WhenAnyAncestorOfType<BindableCard, ILibrary>();
            IoC.Kernel.Bind<IGameAction>().ToConstructor(x => new Draw(drawAmount));
            

            var player = IoC.Kernel.Get<IPlayer>();
            player.Ready();
            player.StartTurn();

            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(
                new CardRepartition(    library: deckSize - 5, 
                                        hand: 5, 
                                        discard: 0, 
                                        battlefield: 0)));

            player.Play(player.Deck.Hand.Cards.First());

            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(
                new CardRepartition(    library: deckSize - 5 - drawAmount,
                                        hand: 4 + drawAmount, 
                                        discard: 0, 
                                        battlefield: 1)));
        }

        [TestCase(1)]
        [TestCase(0)]
        [TestCase(2)]
        [TestCase(10)]
        public void AddAction(int amount)
        {
            IoC.Kernel.BindMultipleTimes<ICard>(10).To<ICard, BindableCard>().WhenAnyAncestorOfType<BindableCard, ILibrary>();
            IoC.Kernel.Bind<IGameAction>().ToConstructor(x => new AddAction(amount));

            var player = IoC.Kernel.Get<IPlayer>();
            player.Ready();
            player.StartTurn();

            Assert.That(player.AvailableActions, Is.EqualTo(1));

            player.Play(player.Deck.Hand.Cards.First());

            Assert.That(player.AvailableActions, Is.EqualTo(amount));
        }


        [TestCase(1)]
        [TestCase(0)]
        [TestCase(2)]
        [TestCase(10)]
        public void AddBuy(int amount)
        {
            IoC.Kernel.BindMultipleTimes<ICard>(10).To<ICard, BindableCard>().WhenAnyAncestorOfType<BindableCard, ILibrary>();
            IoC.Kernel.Bind<IGameAction>().ToConstructor(x => new AddBuy(amount));

            var player = IoC.Kernel.Get<IPlayer>();
            player.Ready();
            player.StartTurn();

            Assert.That(player.AvailableBuys, Is.EqualTo(1));

            player.Play(player.Deck.Hand.Cards.First());

            Assert.That(player.AvailableBuys, Is.EqualTo(1 + amount));
        }



    }
}
