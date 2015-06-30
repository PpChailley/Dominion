﻿using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using Ninject;

namespace gbd.Dominion.Model.Zones
{
    public class SupplyZone: ISupplyZone
    {
        public IList<ISupplyPile> Piles { get; protected set; }

        // TODO: NInject should be able to deal with a ctor asking for ICollection<ICard>
        [Inject]
        protected SupplyZone(IList<ISupplyPile> inputCards)
        {
            Piles = ReorderCards(inputCards);
        }

        private IList<ISupplyPile> ReorderCards(IList<ISupplyPile> input)
        {
            var types = new List<Type>(20);
            var orderedPiles = new List<ISupplyPile>(20);

            foreach (var supplyPile in input)
            {
                types.AddRange(supplyPile.Cards.Select(c => c.GetType()));
            }

            foreach (var t in types)
            {
                var cardsOfType = new List<ICard>(20);
                input.ToList().ForEach(p => 
                    cardsOfType.AddRange(p.Cards.Where(c => c.GetType() == t)));
                orderedPiles.Add(new SupplyPile(cardsOfType));
            }

            return orderedPiles;
        }
        
        public ISupplyPile PileOf<TCardType>()
        {
            return PileOf(typeof (TCardType));
        }

        public ISupplyPile PileOf(Type cardType)
        {
            return Piles.Single(p => p.CardType == cardType);
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
