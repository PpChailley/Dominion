using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.gbd.Dominion.Contents;

namespace org.gbd.Dominion.Model.Cards
{
    public abstract class ConditionalCard: AbstractCard
    {
        

        public override GameSet PresentInSet
        {
            get { return GameSet.Conditional; }
        }
    }
}
