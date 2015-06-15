using System;
using System.Collections.Generic;
using System.Linq;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Tools;
using gbd.Tools.Cli;
using gbd.Tools.NInject;
using Ninject;

namespace gbd.Dominion.Model.Zones
{
    public class SupplyPile: ISupplyPile
    {

        public const int DEFAULT_SUPPLY_PILE_SIZE = 10;

        public IList<ICard> Cards { get; private set; }



        public SupplyPile()
        {
            Cards = IoC.Kernel.Get<IList<ICard>>().Inject(IoC.Kernel, 10);
        }


/*  
 * TODO: remove this (it should be done by NInject)
        public SupplyPile()
        {
            Cards = new List<ICard>();

            for (var i = 0; i < DEFAULT_SUPPLY_PILE_SIZE; i++)
            {
                Cards.Add(IoC.Kernel.Get<ICard>());
            }
        }

  */      


        public IEnumerable<ICard> Get(int amount, Position positionFrom = Position.Top)
        {

            if (amount > Cards.Count)
                throw new NotEnoughCardsException("Cannot get {0} cards, collection has only {1}".Format(amount, Cards.Count));

            switch (positionFrom)
            {
                case Position.Top:
                    return Cards.Take(amount);

                case Position.Bottom:
                    return Cards.TakeLast(amount);

                default:
                    throw new InvalidOperationException();
            }
        }

        public void SortCards(Func<ICard, IComparable> comparer)
        {
            Cards = Cards.OrderBy(comparer).ToList();
        }

        public int TotalCardsAvailable
        {
            get { return Cards.Count; }
        }
    }
}