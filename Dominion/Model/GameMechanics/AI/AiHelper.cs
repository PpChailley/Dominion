using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics.Actions;

namespace gbd.Dominion.Model.GameMechanics.AI
{
    internal static class AiHelper
    {

        internal static int AiValue(this Resources r)
        {
            return r.Potions * 3 
                + r.Money * 2;
        }

    }
}
