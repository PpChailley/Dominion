using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;
using gbd.Dominion.Test.Utilities;
using gbd.Tools.NInject;
using Ninject.Modules;

namespace gbd.Dominion.Contents.Cards
{
    public class IoCCardsModule : NinjectModule
    {
        public override void Load()
        {
            BindAlwaysAvailableCards();
            BindCardsFromBaseGame();
        }

        private void BindAlwaysAvailableCards()
        {
            SetBaseData<Copper>(0, 1, 0);
            SetBaseData<Silver>(3, 2, 0);
            SetBaseData<Gold>(6, 3, 0);
            SetBaseData<Estate>(2, 0, 1);
            SetBaseData<Duchy>(5, 0, 3);
            SetBaseData<Province>(8, 0, 6);


            Kernel.Bind<ICardType>().ToConstructor(x => new CurseType(-1)).WhenAnyAncestorOfType<CurseType, Curse>();
            Kernel.Bind<Resources>().ToConstructor(x => new Resources(0)).WhenAnyAncestorOfType<Resources, Curse>();
        }

        private void BindCardsFromBaseGame()
        {
            SetBaseData<Cellar>(2, 0, 0);
            SetBaseData<Chapel>(2, 0, 0);
            SetBaseData<Moat>(2, 0, 0);

            SetBaseData<Chancellor>(3, 0, 0);
            SetBaseData<Village>(3, 0, 0);
            SetBaseData<Woodcutter>(3, 0, 0);
            SetBaseData<Workshop>(3, 0, 0);

            SetBaseData<Bureaucrat>(4, 0, 0);
            SetBaseData<Feast>(4, 0, 0);
            SetBaseData<Gardens>(4, 0, 0);
            SetBaseData<Militia>(4, 0, 0);
            SetBaseData<Moneylender>(4, 0, 0);
            SetBaseData<Remodel>(4, 0, 0);
            SetBaseData<Smithy>(4, 0, 0);
            SetBaseData<Spy>(4, 0, 0);
            SetBaseData<Thief>(4, 0, 0);
            SetBaseData<ThroneRoom>(4, 0, 0);

            SetBaseData<CouncilRoom>(5, 0, 0);
            SetBaseData<Festival>(5, 0, 0);
            SetBaseData<Laboratory>(5, 0, 0);
            SetBaseData<Library>(5, 0, 0);
            SetBaseData<Market>(5, 0, 0);
            SetBaseData<Mine>(5, 0, 0);
            SetBaseData<Witch>(5, 0, 0);

            SetBaseData<Adventurer>(6, 0, 0);
        }

        private void SetBaseData<T>(int coinsCost, int coinValue, int victory) where T : ICard
        {
            Kernel.Bind<Resources>().ToConstructor(x => new Resources(coinsCost)).WhenAnyAncestorOfType<Resources, T>();


            if (coinValue > 0)
                Kernel.Bind<ICardType>()
                    .ToConstructor(x => new TreasureType(coinValue))
                    .WhenAnyAncestorOfType<TreasureType, T>();

            if (victory > 0)
                //Kernel.Bind<ICardType>().ToConstructor(x => new VictoryType(victory)).WhenAnyAncestorOfType<VictoryType, T>();
                Kernel.Bind<ICardType>()
                    .ToConstructor(x => new VictoryType(victory))
                    .WhenAnyAncestorOfType<VictoryType, T>();
                    //.WhenAnyAncestorMatches(r => 
                    //    r.Request.Target.Member.DeclaringType.IsAssignableFrom(typeof(T))
                    //    );


        }
    }
}
