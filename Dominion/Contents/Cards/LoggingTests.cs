using gbd.Dominion.Test.Utilities;
using NLog;
using NUnit.Framework;

namespace gbd.Dominion.Contents.Cards
{

 


    [TestFixture]
    class LoggingTests : BaseTest
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();

        [Test]
        public void LoggerInit()
        {
            _log.Info("Testing log service");
        }
    }
}
