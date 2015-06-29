using System;
using Ninject.Modules;
using NLog;

namespace gbd.Dominion.Tools
{
    [Obsolete]
    class IoCLoggingModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().To<Logger>();
        }
    }
}