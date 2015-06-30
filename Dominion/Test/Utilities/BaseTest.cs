using gbd.Dominion.Injection;
using NUnit.Framework;

namespace gbd.Dominion.Test.Utilities
{
    [TestFixture]
    public class BaseTest
    {

 
        [SetUp]
        public void SetUp()
        {
            InitIoC();
        }



        private void InitIoC()
        {
            IoC.Kernel = IoC.InitKernel();
        }
    }
}
