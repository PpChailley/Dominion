using System.Net.Mime;
using System.Windows.Forms;
using Ninject;
using NLog;
using NLog.Config;
using NLog.Fluent;
using NLog.Targets;
using NUnit.Framework;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.Model
{
    [TestFixture]
    public class BaseTest
    {

        public IKernel Kernel = new StandardKernel();


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
            // everything is done in static yet
        }
    }
}
