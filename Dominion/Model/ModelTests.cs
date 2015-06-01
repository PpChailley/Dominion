using System.Collections.Generic;
using System.Linq;
using Ninject;
using NUnit.Framework;

namespace org.gbd.Dominion.Model
{
    [TestFixture]
    public class ModelTests
    {
        [Test]
        public void DrawFromBaseDeck()
        {

            TestSetup.Kernel.Bind<IDeck>().To<StartingDeck>();

            IDeck d = TestSetup.Kernel.Get<IDeck>();
            var cards = d.Dequeue(3);

            Assert.That(cards.Count, Is.EqualTo(3));

            Assert.That(d.ToList().Count, Is.EqualTo(7));


        }
    }
}
