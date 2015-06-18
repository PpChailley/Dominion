using System;
using System.Collections.Generic;
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
