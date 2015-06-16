using System.Collections.Generic;
using System.Linq;
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
    public class SmokeTests: BaseTest
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
            var testDeck = IoC.Kernel.Get<EasyToTrackDeck>();

            Assert.That(startingDeck.Cards.Count, Is.GreaterThanOrEqualTo(1));
            Assert.That(testDeck.Cards.Count, Is.GreaterThanOrEqualTo(1));

            Assert.That(startingDeck.Cards.First().GetType(), 
                Is.Not.EqualTo(testDeck.Cards.First().GetType()));
        }



        [Test]
        public void NInjectBindMultipleTimes()
        {
            IoC.Kernel.Unbind<ICard>();
            Assert.That(IoC.Kernel.GetBindings(typeof(ICard)).Count(), Is.EqualTo(0));

            IoC.Kernel.BindMultipleTimes<ICard>(10).To<ICard, TestCard>();
            Assert.That(IoC.Kernel.GetBindings(typeof(ICard)).Count(), Is.EqualTo(10));

            IoC.Kernel.Bind<ICard>().To<TestCard>();
            Assert.That(IoC.Kernel.GetBindings(typeof(ICard)).Count(), Is.EqualTo(11));
        }

    }
}
