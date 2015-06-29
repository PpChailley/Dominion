using Ninject.Modules;
using NLog;

namespace gbd.Dominion.Tools
{
    class IoCLoggingModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().To<Logger>();
        }
    }
}