using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using gbd.Tools.Clr;

namespace gbd.Dominion.Model.Zones
{
    public abstract class AbstractZone: IZone
    {
        protected AbstractZone(IList<ICard> cards)
        {
            Cards = cards;
        }

        public IList<ICard> Cards { get; protected set; }

        public IEnumerable<ICard> Get(int amount, Position positionFrom = Position.Top)
        {
            if (amount > Cards.Count)
            {
                throw new NotEnoughCardsException("Cannot get {0} cards, collection has only {1}"
                    .Format(amount,Cards.Count));
            }


            switch (positionFrom)
            {
                case Position.Top:
                    return Cards.Take(amount).ToList();

                case Position.Bottom:
                    return Cards.TakeLast(amount).ToList();

                default:
                    throw new InvalidOperationException();
            }
        }

        public void SortCards(Func<ICard, IComparable> comparer)
        {
            this.Cards = Cards.OrderBy(comparer).ToList();
        }

        public virtual int TotalCardsAvailable
        {
            get { return Cards.Count; }
        }

        public void Ready()
        {
            Cards.ToList().ForEach(c => c.Ready(this));
        }


        public override string ToString()
        {
            return StringContentsExtension.Format("{0} # {3} with {2}/{1} cards)", 
                                this.GetType().Name, 
                                this.TotalCardsAvailable,
                                this.Cards.Count,
                                this.GetHashCode());
        }
    }
}