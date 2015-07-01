using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
using gbd.Tools.NInject;
using Ninject;

namespace gbd.Dominion.Injection
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
            k.BindTo<ICard, TDest>(amount).WhenAnyAncestorOfType<TDest, TContainer>();
        }
    }
}
