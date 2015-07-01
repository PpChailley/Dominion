using gbd.Dominion.Contents;
using gbd.Dominion.Contents.Cards;
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
            this.Kernel.BindTo<ICard, EmptyCard>(10).WhenInto<EmptyCard, ILibrary>();
            this.Kernel.BindTo<ICard, EmptyCard>(10).WhenInto<EmptyCard, ISupplyZone>();
            this.Kernel.BindTo<ICard, EmptyCard>(10).WhenInto<EmptyCard, ISupplyPile>();


            // Those are not bindable, no need to bind them :)
            //Kernel.Bind<GameExtension>().ToConstant(GameExtension.TestCards).WhenInto<GameExtension, EmptyCard>();
            //Kernel.Bind<Include>().ToConstant(Include.TestCards).WhenInto<Include, EmptyCard>();

            Kernel.Bind<GameExtension>().ToConstant(GameExtension.TestCards).WhenInto<GameExtension, BindableCard>();
            Kernel.Bind<Include>().ToConstant(Include.TestCards).WhenInto<Include, BindableCard>();


            this.Kernel.BindTo<ISupplyPile, SupplyPile>(10);
            

            Kernel.Bind<ICardShuffler>().To<CardShufflerRandom>();

        }

    }
}