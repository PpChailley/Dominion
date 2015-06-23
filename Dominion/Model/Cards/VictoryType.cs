using System;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Model.Cards
{
    public class VictoryType : ICardType
    {
        private readonly int? _victoryPoints = null;
        private readonly Func<IZone, int> _computeVictory;
        private IZone _zone;

        public int VictoryPoints
        {
            get
            {
                if (_victoryPoints == null)
                    return _computeVictory(_zone);
                else 
                    return (int) _victoryPoints;
            }
        }

        public VictoryType(int points)
        {
            if (points < 0)
                throw new InvalidOperationException("Victory cards must have a positive points value");

            _victoryPoints = points;
        }

        public VictoryType(Func<IZone, int> computeVictory)
        {
            _victoryPoints = null;
            _computeVictory = computeVictory;
        }

        public void Ready(IZone zone)
        {
            _zone = zone;
        }
    }
}