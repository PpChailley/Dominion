using System;
using System.Linq;
using Moq;
using Ninject;
using NUnit.Framework;
using org.gbd.Dominion.AI;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.Model
{
    [TestFixture]
    public class AiTest : BaseTest
    {
        [Test]
        public void EnoughAiImplemented()
        {
            Assert.That(ReflectionClassFinder.GetAllAiTestCaseData().Count(), Is.GreaterThan(0));
        }

        [Test, TestCaseSource(typeof (ReflectionClassFinder), "GetAllAiTestCaseData")]
        public void AiKnowsWhatToDiscard(Type aiType)
        {

            throw new NotImplementedException();
            IoC.ReBind<IAi>().To(aiType);
            var ai = IoC.Kernel.Get<IAi>();

            var playerMock = new Mock<Player>();
            playerMock.Setup(p => p.Deck);


            ai.Init(playerMock.Object);

            var toDiscard = ai.ChooseAndDiscard(3);




        }


        [Test, TestCaseSource(typeof(ReflectionClassFinder), "GetAllAiTestCaseData")]
        [Repeat(30)]
        public void AiIsAbleToDiscard(Type ai)
        {
            IoC.ReBind<IDeck>().To<EasyToTrackDeck>();
            IoC.ReBind<IAi>().To(ai);

            var player = IoC.Kernel.Get<Player>();
            player.GetReadyToStartGame();
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5,5,0,0)));

            player.DiscardFromHand(0);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5, 5, 0, 0)));

            player.DiscardFromHand(1);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5, 4, 1, 0)));

            player.DiscardFromHand(2);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(5, 2, 3, 0)));

            player.Draw(1);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(4, 3, 3, 0)));

            player.DiscardFromHand(2);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(4, 1, 5, 0)));

            player.Draw(6);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(3, 7, 0, 0)));

            player.DiscardFromHand(5);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(3, 2, 5, 0)));

            player.Draw(1);
            Assert.That(player.Deck.CardCountByZone, Is.EqualTo(new CardRepartition(2, 3, 5, 0)));
        }

 



    }


}