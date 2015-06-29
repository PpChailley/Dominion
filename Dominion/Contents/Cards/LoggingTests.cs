using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gbd.Dominion.Test.Utilities;
using Ninject;
using NLog;
using NLog.Fluent;
using NUnit.Framework;

namespace gbd.Dominion.Contents.Cards
{

 


    [TestFixture]
    class LoggingTests: BaseTest
    {
        

        [Test]
        public void LoggerInit()
        {
            Log.Info("Testing log service");
        }
    }
}
