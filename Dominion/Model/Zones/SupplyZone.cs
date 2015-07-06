using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Contents.Cards;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;
using Ninject;
using NLog;

namespace gbd.Dominion.Model.Zones
{
    public class SupplyZone: ISupplyZone
    {
        private const int EMPTY_PILES_NEEDED_TO_END_GAME = 3;

        
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();



        public IList<ISupplyPile> Piles { get; protected set; }

        public bool EndOfGameCondition
        {
            get
            {
                if (PileOf<Province>() != null
                    && PileOf<Province>().Cards.Any() == false)
                {
                    Log.Info("No more provinces - End of game reached");
                    return true;
                }

                if (Piles.Count(p => p.Cards.Any() == false) >= EMPTY_PILES_NEEDED_TO_END_GAME)
                {
                    Log.Info("Many empty pailes - End of game reached");
                    return true;
                }

                return false;
            }
        }

        [Inject]
        public SupplyZone(IEnumerable<ICard> inputCards)
        {
            Piles = ReorderCards(inputCards);
        }

        private IList<ISupplyPile> ReorderCards(IEnumerable<ICard> input)
        {
            var types = input.Select(c => c.GetType()).Distinct();
            var orderedPiles = new List<ISupplyPile>(20);

            foreach (var t in types)
            {
                var cardsOfType = new List<ICard>(20);
                cardsOfType.AddRange(input.Where(c => c.GetType() == t));
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
            return Piles.SingleOrDefault(p => p.CardType == cardType);
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
