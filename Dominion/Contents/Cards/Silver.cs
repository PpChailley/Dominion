using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.gbd.Dominion.Model;
using org.gbd.Dominion.Model.Cards;

namespace org.gbd.Dominion.Contents
{
    public class Silver : BaseSetCard
    {
        public Silver()
        {
            Mechanics.Cost = new Resources(3);
            Mechanics.Types.Add(new Treasure(2));
        }
    }
}
