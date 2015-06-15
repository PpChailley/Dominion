using NUnit.Framework;
using gbd.Dominion.Tools;

namespace gbd.Dominion.Model
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
            Logging.InitProgrammaticallyToDefault();
        }

        private void InitIoC()
        {
            IoC.Kernel = IoC.InitKernel();
        }
    }
}
