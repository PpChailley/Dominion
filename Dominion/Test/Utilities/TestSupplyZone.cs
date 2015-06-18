using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gbd.Dominion.Model;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Test.Utilities
{
    public class TestSupplyZone : AbstractSupplyZone, ISupplyZone
    {


        public override void MakeReadyToStartGame()
        {
            throw new NotImplementedException();
        }

        public override void GetReadyToPlay()
        {
            throw new NotImplementedException();
        }

        public TestSupplyZone(IList<ISupplyPile> piles) : base(piles)
        {
        }
    }
}
