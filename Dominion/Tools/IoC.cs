using System;
using System.Linq;
using Ninject;
using Ninject.Syntax;

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
                                            new IoCTestModule());

            return _kernel;
        }

        



        public static IBindingToSyntax<T> ReBind<T>()
        {
            int nbBindingsDefined = Kernel.GetBindings(typeof (T)).Count();
            if (nbBindingsDefined != 1)
            {
                throw new InvalidOperationException("Rebind() should only be called when exactly one binding is defined\n" +
                                                    "  Defined: " + nbBindingsDefined + "\n" +
                                                    "  0  - Binding is missing from normal operations declaration\n" +
                                                    "  >1 - There is trash in here");
            }

            Kernel.Unbind<T>();
            return Kernel.Bind<T>();
        }
    }
}
