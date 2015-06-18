using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Model;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Test.Utilities;
using gbd.Dominion.Tools;
using gbd.Tools.NInject;
using Ninject;
using NUnit.Framework;

namespace gbd.Dominion.Test.Scenarios
{
    [TestFixture]
    public class NInjectTests: BaseTest
    {

        [Test]
        public void SmokeTest()
        {
            var player = IoC.Kernel.Get<IPlayer>();
        }


        [Test]
        public void InjectionDifferBetweenDeckImplementations()
        {
            var startingDeck = IoC.Kernel.Get<StartingDeck>();
            var testDeck = IoC.Kernel.Get<TestDeck>();

            Assert.That(startingDeck.Cards.Count, Is.GreaterThanOrEqualTo(1));
            Assert.That(testDeck.Cards.Count, Is.GreaterThanOrEqualTo(1));

            Assert.That(startingDeck.Cards.First().GetType(), 
                Is.Not.EqualTo(testDeck.Cards.First().GetType()));
        }


    }
}
