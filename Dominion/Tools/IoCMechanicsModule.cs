using System.Collections.Generic;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;
using gbd.Dominion.Model.Zones;
using Ninject.Modules;

namespace gbd.Dominion.Tools
{
    public class IoCMechanicsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<int>().ToConstant(0);

            Bind<IHand>().To<Hand>();
            Bind<IDiscardPile>().To<DiscardPile>();
            Bind<ILibrary>().To<Library>();
            Bind<IBattleField>().To<BattleField>();
            Bind<IPlayer>().To<Player>();

            Bind<IGame>().To<Game>().InSingletonScope();

            Bind<ICardMechanics>().To<CardMechanics>();

            Bind<CursePile>().ToSelf().InSingletonScope();

            //Bind<ICollection<IPlayer>>().ToConstructor(x => new List<IPlayer>(x.Inject<IList<IPlayer>>()));


            // TODO: move this to a specific Module
            Kernel.Bind<ICardShuffler>().To<CardShufflerRandom>();

        }

    }
}