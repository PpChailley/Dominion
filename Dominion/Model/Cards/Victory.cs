using System;

namespace gbd.Dominion.Model.Cards
{
    public class Victory : ICardType
    {
        public int VictoryPoints;

        public Victory(int points)
        {
            if (points < 0)
                throw new InvalidOperationException("Victory cards must have a positive points value");

            VictoryPoints = points;
        }
    }
}