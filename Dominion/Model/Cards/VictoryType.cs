using System;

namespace gbd.Dominion.Model.Cards
{
    public class VictoryType : ICardType
    {
        public int VictoryPoints;

        public VictoryType(int points)
        {
            if (points < 0)
                throw new InvalidOperationException("Victory cards must have a positive points value");

            VictoryPoints = points;
        }
    }
}