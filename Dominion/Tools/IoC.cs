using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model;
using gbd.Tools.Cli;
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

        
    }
}
