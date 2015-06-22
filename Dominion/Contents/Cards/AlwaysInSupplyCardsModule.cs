using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.Actions;
using gbd.Tools.NInject;
using Ninject.Modules;

namespace gbd.Dominion.Contents.Cards
{
    public class AlwaysInSupplyCardsModule: NinjectModule
    {
        public override void Load()
        {

            SetBaseData<Copper>(0, 1, 0);
            SetBaseData<Silver>(3, 2, 0);
            SetBaseData<Gold>(6, 3, 0);
            SetBaseData<Estate>(2, 0, 1);
            SetBaseData<Duchy>(5, 0, 3);
            SetBaseData<Province>(8, 0, 6);

            // Copper
            //Kernel.Bind<ICardType>().ToConstructor(x => new TreasureType(1)).WhenAnyAncestorOfType<TreasureType, Copper>();
            //Kernel.Bind<Resources>().ToConstructor(x => new Resources(0)).WhenAnyAncestorOfType<Resources, Copper>();





        }

        private void SetBaseData<T>(int coinsCost, int coinValue, int victory) where T:ICard
        {
            Kernel.Bind<Resources>().ToConstructor(x => new Resources(coinsCost)).WhenAnyAncestorOfType<Resources, T>();
            
            
            if (coinValue > 0)
                Kernel.Bind<ICardType>().ToConstructor(x => new TreasureType(coinValue)).WhenAnyAncestorOfType<TreasureType, T>();

            if (victory > 0)
                Kernel.Bind<ICardType>().ToConstructor(x => new VictoryType(victory)).WhenAnyAncestorOfType<VictoryType, T>();
        }
    }
}
