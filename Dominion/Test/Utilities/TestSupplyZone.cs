using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gbd.Dominion.Model;
using gbd.Dominion.Model.Zones;

namespace gbd.Dominion.Test.Utilities
{
    public class TestSupplyZone: ISupplyZone
    {
        public IList<ICard> Cards
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<ICard> Get(int amount, Position positionFrom = Position.Top)
        {
            throw new NotImplementedException();
        }

        public void SortCards(Func<ICard, IComparable> comparer)
        {
            throw new NotImplementedException();
        }

        public int TotalCardsAvailable
        {
            get { throw new NotImplementedException(); }
        }

        public void MakeReadyToStartGame()
        {
            throw new NotImplementedException();
        }
    }
}
