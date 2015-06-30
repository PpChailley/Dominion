using System;
using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;
using gbd.Tools.NInject;
using Ninject.Modules;

namespace gbd.Dominion.Tools
{
    public class IoCStandardGameModule : NinjectModule
    {
        public override void Load()
        {
            //Bind<IIntelligence>().To<RandomAi>();
            //Bind<IAi>().To<RandomAi>();
            //Bind<ISupplyPile>().To<TestSupplyPile>();
            Bind<ISupplyZone>().To<SupplyZone>();
            //Bind<IDeck>().To<TestDeck>();


            Kernel.BindMultipleTimesTo<ICard, Copper>(7).WhenAnyAncestorOfType<Copper, ILibrary>();
            Kernel.BindMultipleTimesTo<ICard, Estate>(3).WhenAnyAncestorOfType<Estate, ILibrary>();

            Kernel.BindMultipleTimesTo<ICard, Copper>(10).WhenAnyAncestorOfType<Copper, ISupplyPile>();
            Kernel.BindMultipleTimesTo<ICard, Silver>(10).WhenAnyAncestorOfType<Silver, ISupplyPile>();
            Kernel.BindMultipleTimesTo<ICard, Gold>(10).WhenAnyAncestorOfType<Gold, ISupplyPile>();
            Kernel.BindMultipleTimesTo<ICard, Estate>(10).WhenAnyAncestorOfType<Estate, ISupplyPile>();
            Kernel.BindMultipleTimesTo<ICard, Duchy>(10).WhenAnyAncestorOfType<Duchy, ISupplyPile>();
            Kernel.BindMultipleTimesTo<ICard, Province>(10).WhenAnyAncestorOfType<Province, ISupplyPile>();
            Kernel.BindMultipleTimesTo<ICard, Curse>(10).WhenAnyAncestorOfType<Curse, ISupplyPile>();


        }
    }
}
