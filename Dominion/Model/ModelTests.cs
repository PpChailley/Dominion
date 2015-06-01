using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace org.gbd.Dominion.Model
{
    [TestFixture]
    public class ModelTests
    {
        [Test]
        public void DrawFromBaseDeck()
        {

            

            this.Bind<IDeck>().To<StartingDeck>();
            Deck d =
        }

    }
}
