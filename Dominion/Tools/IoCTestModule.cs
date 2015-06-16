using System;
using System.Linq;
using gbd.Dominion.AI;
using gbd.Dominion.Model;
using gbd.Dominion.Model.Zones;
using gbd.Dominion.Test.Utilities;
using gbd.Tools.NInject;
using Ninject.Modules;
using NUnit.Framework;

namespace gbd.Dominion.Tools
{
    public class IoCTestModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IIntelligence>().To<RandomAi>();
            Bind<IAi>().To<RandomAi>();
            Bind<ISupplyPile>().To<TestSupplyPile>();
            Bind<ISupplyZone>().To<TestSupplyZone>();
            Bind<IDeck>().To<EasyToTrackDeck>();
            Bind<ISupplyPile>().To<TestSupplyPile>();
            Bind<ISupplyZone>().To<TestSupplyZone>();

            //foreach (var binding in this.BindMultipleTimes<ICard>(10))
            //{
            //    binding.To<TestCard>().WhenInjectedInto<ILibrary>();
            //}


            //this.BindMultipleTimes<ICard>(10).ForEach( 
            //    x => x.To<TestCard>().WhenInjectedInto<ILibrary>());

            this.BindMultipleTimesTo<ICard, TestCard>(10, x => x.WhenInjectedInto<ILibrary>());





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