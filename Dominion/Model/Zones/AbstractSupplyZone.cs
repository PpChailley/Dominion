using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Model.Zones
{
    public abstract class AbstractSupplyZone: ISupplyZone
    {
        public IList<ISupplyPile> Piles { get; protected set; }

        
        protected AbstractSupplyZone(IList<ISupplyPile> piles)
        {
            Piles = piles;
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

        public abstract void MakeReadyToStartGame();

        public abstract void GetReadyToPlay();

        
    }
}
