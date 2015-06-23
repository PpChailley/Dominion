using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using Ninject;

namespace gbd.Dominion.Model.Zones
{
    public abstract class AbstractSupplyZone: ISupplyZone
    {
        public IList<ISupplyPile> Piles { get; protected set; }

        public CursePile CursePile { get; protected set; }


        [Inject]
        protected AbstractSupplyZone(IList<ISupplyPile> piles, CursePile cursePile)
        {
            Piles = piles;
            CursePile = cursePile;
        }


        public IList<ICard> Cards
        {
            get
            {
                IList<ICard> toreturn = Piles.SelectMany(pile => pile.Cards).ToList();
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

        public void Ready()
        {
            // Do nothing
        }

        

        
    }
}
