using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.Zones;
using gbd.Tools.NInject;
using Ninject.Modules;

namespace gbd.Dominion.Injection
{
    public class IoCStandardGameModule : NinjectModule
    {
        public const int NB_COPPER = 40;
        public const int NB_SILVER = 40;
        public const int NB_GOLD = 30;
        public const int NB_ESTATE = 20;
        public const int NB_DUCHY = 12;
        public const int NB_PROVINCE = 12;
        public const int NB_CURSE = 30;



        public override void Load()
        {
            Bind<ISupplyZone>().To<SupplyZone>();
            Bind<IDeck>().To<StartingDeck>();

            Kernel.BindTo<ICard, Copper>(7).WhenAnyAncestorOfType<Copper, ILibrary>();
            Kernel.BindTo<ICard, Estate>(3).WhenAnyAncestorOfType<Estate, ILibrary>();

            Kernel.BindTo<ICard, Copper>(NB_COPPER).WhenAnyAncestorOfType<Copper, ISupplyZone>();
            Kernel.BindTo<ICard, Silver>(NB_SILVER).WhenAnyAncestorOfType<Silver, ISupplyZone>();
            Kernel.BindTo<ICard, Gold>(NB_GOLD).WhenAnyAncestorOfType<Gold, ISupplyZone>();
            Kernel.BindTo<ICard, Estate>(NB_ESTATE).WhenAnyAncestorOfType<Estate, ISupplyZone>();
            Kernel.BindTo<ICard, Duchy>(NB_DUCHY).WhenAnyAncestorOfType<Duchy, ISupplyZone>();
            Kernel.BindTo<ICard, Province>(NB_PROVINCE).WhenAnyAncestorOfType<Province, ISupplyZone>();
            Kernel.BindTo<ICard, Curse>(NB_CURSE).WhenAnyAncestorOfType<Curse, ISupplyZone>();

            Kernel.Bind<ICardShuffler>().To<CardShufflerRandom>();

        }
    }
}
