using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;
using gbd.Tools.NInject;
using Ninject;
using NLog;

namespace gbd.Dominion.Tools
{
    public static class IoC
    {

        private static IKernel _kernel = null;

        public static IKernel Kernel
        {
            get { return _kernel ?? (_kernel = InitKernel()); }
            set { _kernel = value; }
        }

        public static IKernel InitKernel()
        {
            _kernel = new StandardKernel(   new IoCMechanicsModule(),
                                            new IoCTestModule(),
                                            new IoCCardsModule()
                );

            // LogManager.GetCurrentClassLogger().Trace("Kernel loaded all modules");

            return _kernel;
        }


        public static void BindCard<TDest, TContainer>(this IKernel k, int amount)
            where TDest : ICard
            where TContainer : IZone
        {
            k.BindMultipleTimesTo<ICard, TDest>(amount).WhenAnyAncestorOfType<TDest, TContainer>();
        }
    }
}
