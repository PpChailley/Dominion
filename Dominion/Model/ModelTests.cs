using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            var d = TestSetup.Kernel.Get<IDeck>();

            d.Draw();


        }
    }
}
