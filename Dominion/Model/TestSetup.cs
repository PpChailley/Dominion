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
    public class TestSetup
    {

        public static IKernel Kernel = new StandardKernel();


        [TestFixtureSetUp]
        public void Init()
        {
            InitLogging();
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
