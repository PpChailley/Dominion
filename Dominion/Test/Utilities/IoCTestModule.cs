using gbd.Dominion.Model;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.AI;
using gbd.Dominion.Model.Zones;
using gbd.Tools.NInject;
using Ninject.Modules;

namespace gbd.Dominion.Test.Utilities
{
    public class IoCTestModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IIntelligence>().To<RandomAi>();
            Bind<IAi>().To<RandomAi>();
            Bind<ISupplyZone>().To<TestSupplyZone>();

            Bind<IDeck>().To<TestDeck>();
            this.Kernel.BindMultipleTimesTo<ICard, EmptyCard>(10).WhenAnyAncestorOfType<EmptyCard, ILibrary>();
            this.Kernel.BindMultipleTimesTo<ICard, EmptyCard>(10).WhenAnyAncestorOfType<EmptyCard, ISupplyZone>();
            this.Kernel.BindMultipleTimesTo<ICard, EmptyCard>(10).WhenAnyAncestorOfType<EmptyCard, ISupplyPile>();


            this.Kernel.BindMultipleTimesTo<ISupplyPile, SupplyPile>(10);
            

            Kernel.Bind<ICardShuffler>().To<CardShufflerRandom>();

        }

    }
}