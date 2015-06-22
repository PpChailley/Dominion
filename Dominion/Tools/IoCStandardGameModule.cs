using System;
using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.Zones;
using gbd.Tools.NInject;
using Ninject.Modules;

namespace gbd.Dominion.Tools
{
    public class IoCStandardGameModule: NinjectModule
    {
        public override void Load()
        {
            //Bind<IIntelligence>().To<RandomAi>();
            //Bind<IAi>().To<RandomAi>();
            //Bind<ISupplyPile>().To<TestSupplyPile>();
            //Bind<ISupplyZone>().To<TestSupplyZone>();
            //Bind<IDeck>().To<TestDeck>();


            this.Kernel.BindMultipleTimesTo<ICard, Copper>(7).WhenAnyAncestorOfType<Copper, IDeck>();
            this.Kernel.BindMultipleTimesTo<ICard, Estate>(3).WhenAnyAncestorOfType<Estate, IDeck>();

            throw new NotImplementedException("Not ready to start standard game: \n" +
                                              "  Need to init supply pile");
        }
    }
}
