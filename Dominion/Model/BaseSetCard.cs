using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.gbd.Dominion.Model
{
    public abstract class BaseSetCard: AbstractCard
    {
        public override GameSet PresentInSet
        {
            get { return GameSet.BaseSet; }
        }
    }
}
