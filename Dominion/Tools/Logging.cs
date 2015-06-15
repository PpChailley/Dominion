using System.Windows.Forms;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace gbd.Dominion.Tools
{
    public class Logging
    {

        private static Logger Log = LogManager.GetCurrentClassLogger();


        public static void InitProgrammaticallyToDefault()
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = Application.ProductName + ".nlog" };
            config.AddTarget("logfile", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, target));
            LogManager.Configuration = config;

            Log = LogManager.GetCurrentClassLogger();

            Log.Info("NLog Initialized");
        }
    }
}