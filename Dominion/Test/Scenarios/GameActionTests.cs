using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
using gbd.Dominion.Tools;
using gbd.Tools.NInject;
using Ninject;
using Ninject.Syntax;
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

            //IoC.Kernel.Unbind<ICardMechanics>();

            //IoC.Kernel.Unbind<IDeck>();
            //IoC.Kernel.Unbind<IHand>();
            //IoC.Kernel.Unbind<IDiscardPile>();
            //IoC.Kernel.Unbind<ILibrary>();
            //IoC.Kernel.Unbind<IBattleField>();

            

        }

        [ExpectedException(typeof (NotEnoughCardsException))]
        [TestCase(10, 6)]
        [TestCase(1, 6)]
        [TestCase(100, 96)]
        [TestCase(0, 1)]
        public void DrawRobustness(int deckSize, int drawAmount)
        {
            Draw(deckSize, drawAmount);
        }
        
        [TestCase(10, 1)]
        [TestCase(10, 2)]
        [TestCase(10, 5)]
        [TestCase(10, 1)]
        public void Draw(int deckSize, int drawAmount)
        {
            IoC.Kernel.Unbind<IPlayer>();
            IoC.Kernel.Bind<IPlayer>().To<Player>().InSingletonScope();

            IoC.Kernel.BindMultipleTimes<ICard>(deckSize).To<ICard, BindableCard>().WhenAnyAncestorOfType<BindableCard, ILibrary>();
            IoC.Kernel.Bind<IGameAction>().ToConstructor(x => new Draw(drawAmount));
            
            

            var player = IoC.Kernel.Get<IPlayer>();
            player.Ready();

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



    }
}
