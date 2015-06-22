﻿using System;
using System.Linq;
using gbd.Dominion.Model;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.AI;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
using gbd.Dominion.Tools;
using gbd.Tools.NInject;
using Ninject;
using NUnit.Framework;

namespace gbd.Dominion.Test.Scenarios
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
            IoC.Kernel.ReBind<IDeck>().To<TestDeck>();
            IoC.Kernel.ReBind<IAi>().To(ai);

            var player = IoC.Kernel.Get<Player>();
            player.Ready();
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