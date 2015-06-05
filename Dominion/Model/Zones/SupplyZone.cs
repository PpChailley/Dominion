using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.Model.Zones
{
    public abstract class AbstractSupplyZone: ISupplyZone
    {


        public IList<ISupplyPile> Piles = IoC.Kernel.Get<IList<ISupplyPile>>();


        public IList<ICard> Cards
        {
            get
            {
                IList<ICard> toreturn = Piles.SelectMany(pile => pile).ToList();
                return toreturn;
            }
        }

        public IEnumerable<ICard> Get(int amount, Position positionFrom = Position.Top)
        {
            throw new InvalidOperationException("Can't Choose card from supplyZone");
        }

        public void SortCards(Func<ICard, IComparable> comparer)
        {
            throw new InvalidOperationException("Can't Choose card from supplyZone");
        }

        public int TotalCardsAvailable
        {
            get { return this.Cards.Count; }
        }

        public abstract void GetReadyToPlay();

        
    }
}
