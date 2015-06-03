using System;
using System.Linq;
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

        [Test, TestCaseSource(typeof(ReflectionClassFinder), "GetAllAiTestCaseData")]
        public void AiIsAbleToDiscard(Type ai)
        {
            IoC.ReBind<IAi>().To(ai);

            var player = IoC.Kernel.Get<Player>();
            player.GetReadyToStartGame();
            Assert.That(player.Hand.Cards.Count, Is.EqualTo(5));

            player.DiscardFromHand(0);
            Assert.That(player.Hand.Cards.Count, Is.EqualTo(5));

            player.DiscardFromHand(1);
            Assert.That(player.Hand.Cards.Count, Is.EqualTo(4));

            player.DiscardFromHand(2);
            Assert.That(player.Hand.Cards.Count, Is.EqualTo(2));

            player.Draw(1);
            Assert.That(player.Hand.Cards.Count, Is.EqualTo(3));

            player.DiscardFromHand(2);
            Assert.That(player.Hand.Cards.Count, Is.EqualTo(1));

            player.Draw(6);
            Assert.That(player.Hand.Cards.Count, Is.EqualTo(7));

            player.DiscardFromHand(5);
            Assert.That(player.Hand.Cards.Count, Is.EqualTo(2));

            player.Draw(1);
            Assert.That(player.Hand.Cards.Count, Is.EqualTo(3));
        }

    }
}