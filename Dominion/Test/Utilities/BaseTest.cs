using gbd.Dominion.Tools;
using NUnit.Framework;

namespace gbd.Dominion.Test.Utilities
{
    [TestFixture]
    public class BaseTest
    {

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            InitLogging();
            InitIoC();
        }


        [SetUp]
        public void SetUp()
        {
            InitIoC();
        }





        private void InitLogging()
        {
            //Logging.InitProgrammaticallyToDefault();
        }

        private void InitIoC()
        {
            IoC.Kernel = IoC.InitKernel();
        }
    }
}
