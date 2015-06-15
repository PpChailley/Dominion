using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Tools;
using gbd.Tools.Cli;
using gbd.Tools.NInject;
using Ninject;

namespace gbd.Dominion.Model.Zones
{
    public class SupplyPile : AbstractZone, ISupplyPile 
    {

        public const int DEFAULT_SUPPLY_PILE_SIZE = 10;



        public SupplyPile()
        {
            Cards = IoC.Kernel.Get<IList<ICard>>().Inject(IoC.Kernel, 10);
        }


/*  
 * TODO: remove this (it should be done by NInject)
        public SupplyPile()
        {
            Cards = new List<ICard>();

            for (var i = 0; i < DEFAULT_SUPPLY_PILE_SIZE; i++)
            {
                Cards.Add(IoC.Kernel.Get<ICard>());
            }
        }

  */      


 
    }
}