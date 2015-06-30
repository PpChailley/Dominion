using System;
using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.Zones;
using gbd.Tools.NInject;
using Ninject.Modules;

namespace gbd.Dominion.Tools
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

            Kernel.BindMultipleTimesTo<ICard, Copper>(7).WhenAnyAncestorOfType<Copper, ILibrary>();
            Kernel.BindMultipleTimesTo<ICard, Estate>(3).WhenAnyAncestorOfType<Estate, ILibrary>();

            Kernel.BindMultipleTimesTo<ICard, Copper>(NB_COPPER).WhenAnyAncestorOfType<Copper, ISupplyZone>();
            Kernel.BindMultipleTimesTo<ICard, Silver>(NB_SILVER).WhenAnyAncestorOfType<Silver, ISupplyZone>();
            Kernel.BindMultipleTimesTo<ICard, Gold>(NB_GOLD).WhenAnyAncestorOfType<Gold, ISupplyZone>();
            Kernel.BindMultipleTimesTo<ICard, Estate>(NB_ESTATE).WhenAnyAncestorOfType<Estate, ISupplyZone>();
            Kernel.BindMultipleTimesTo<ICard, Duchy>(NB_DUCHY).WhenAnyAncestorOfType<Duchy, ISupplyZone>();
            Kernel.BindMultipleTimesTo<ICard, Province>(NB_PROVINCE).WhenAnyAncestorOfType<Province, ISupplyZone>();
            Kernel.BindMultipleTimesTo<ICard, Curse>(NB_CURSE).WhenAnyAncestorOfType<Curse, ISupplyZone>();


        }
    }
}
