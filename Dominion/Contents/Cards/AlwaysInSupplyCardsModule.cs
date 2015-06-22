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
            // Copper
            Kernel.Bind<ICardType>().ToConstructor(x => new TreasureType(1)).WhenAnyAncestorOfType<TreasureType, Copper>();
            Kernel.Bind<Resources>().ToConstructor(x => new Resources(0)).WhenAnyAncestorOfType<Resources, Copper>();


        }
    }
}
