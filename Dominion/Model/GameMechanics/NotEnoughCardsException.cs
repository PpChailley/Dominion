using System;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.GameMechanics
{
    public class NotEnoughCardsException : Exception
    {
        public NotEnoughCardsException(string s) : base(s){ }

        public NotEnoughCardsException() { }

        public NotEnoughCardsException(IZone zone, int nbAvailableCards, int required)
            : base(String.Format("Not Enough cards in {0}: expected {1}, Status. {2}",
            zone, required, nbAvailableCards))
        { }
    }
}