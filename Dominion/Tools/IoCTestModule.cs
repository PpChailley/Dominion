using System;
using gbd.Dominion.AI;
using gbd.Dominion.Model;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
using Ninject.Modules;

namespace gbd.Dominion.Tools
{
    public class IoCTestModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IIntelligence>().To<RandomAi>();
            Bind<IAi>().To<RandomAi>();
            Bind<ICard>().To<TestCard>();



            Bind<ISupplyPile>().To<TestSupplyPile>();
            Bind<ISupplyZone>().To<TestSupplyZone>();

//            Bind<ISupplyPile>().ToMethod(x => 
//                    new TestSupplyPile())
            

            // TODO: Understand contextual binding (!)
            /*
            Bind<ICard>().To<TestCard>().WhenInjectedInto<ISupplyPile>();
            Bind<ICard>().To<TestCard>().WhenClassHas<InSupplyAttribute>();
            Bind<ICard>().To<TestCard>().WhenTargetHas<InSupplyAttribute>();
             * 
             public class InSupplyAttribute: Attribute{}
             * */

        }
    }
}