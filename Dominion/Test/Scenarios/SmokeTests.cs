using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Test.Utilities;
using gbd.Dominion.Tools;
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


    }
}
