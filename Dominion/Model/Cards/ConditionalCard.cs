using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gbd.Dominion.Contents;

namespace gbd.Dominion.Model.Cards
{
    public abstract class ConditionalCard: Card
    {
        

        public override GameSet PresentInSet
        {
            get { return GameSet.Conditional; }
        }
    }
}
