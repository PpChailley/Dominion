using gbd.Dominion.Contents.Cards;
using Ninject;

namespace gbd.Dominion.Tools
{
    public class IoC
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
                                            new AlwaysInSupplyCardsModule()
                                            );

            return _kernel;
        }

        
    }
}
