using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using Ninject.Syntax;
using org.gbd.Dominion.AI;
using org.gbd.Dominion.Model;
using org.gbd.Dominion.Model.GameMechanics;
using org.gbd.Dominion.Model.Zones;

namespace org.gbd.Dominion.Tools
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
            _kernel = new StandardKernel();

            _kernel.Bind<int>().ToConstant(0);

            _kernel.Bind<IHand>().To<Hand>();
            _kernel.Bind<IDeck>().To<StartingDeck>();
            _kernel.Bind<IDiscardPile>().To<DiscardPile>();
            _kernel.Bind<ILibrary>().To<Library>();
            _kernel.Bind<IBattleField>().To<BattleField>();

            _kernel.Bind<IIntelligence>().To<RandomAi>();

        //    _kernel.Bind<IEnumerator<ICard>>().To<AbstractDeck.DeckSimpleEnumerator>();

            _kernel.Bind<IAi>().To<RandomAi>();
            _kernel.Bind<IPlayer>().To<Player>();
            _kernel.Bind<IGame>().To<Game>();
            
            _kernel.Bind<ICollection<IPlayer>>()
                            .ToConstructor(x => new List<IPlayer>(x.Inject<IList<IPlayer>>()));

            _kernel.Bind<ISupplyPile>().To<SupplyPile>();
            
            

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
