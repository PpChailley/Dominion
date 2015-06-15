using System.Collections.Generic;
using gbd.Dominion.Model;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.Zones;
using gbd.Tools.NInject;
using Ninject.Modules;

namespace gbd.Dominion.Tools
{
    public class IoCMechanicsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<int>().ToConstant(0);

            Bind<IHand>().To<Hand>();
            Bind<IDeck>().To<StartingDeck>();
            Bind<IDiscardPile>().To<DiscardPile>();
            Bind<ILibrary>().To<Library>();
            Bind<IBattleField>().To<BattleField>();
            Bind<IPlayer>().To<Player>();
            Bind<IGame>().To<Game>();

            // Bind<ICollection<IPlayer>>().ToConstructor(x => new List<IPlayer>(x.Inject<IList<IPlayer>>()));
            Bind<ICollection<IPlayer>>().ToConstructor(x => new List<IPlayer>(x.Inject<IList<IPlayer>>()));


            Bind<ICollection<ICard>>().ToConstructor(x =>new List<ICard>(x.Inject<IList<ICard>>()));
            Bind<IList<ICard>>().ToConstructor(x => new List<ICard>(x.Inject<IList<ICard>>()));
            //Bind<IList<ICard>>().To<List<ICard>>();

        }
    }
}