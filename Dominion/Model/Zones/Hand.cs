using System.Collections.Generic;
using gbd.Dominion.Model.Cards;

namespace gbd.Dominion.Model.Zones
{
    public class Hand : AbstractZone, IHand
    {

        public Hand(IList<ICard> cards) : base(cards) { }
        public Hand() : base(new List<ICard>()) { }


        public new IList<ICard> Cards
        {
            get { return base.Cards; }
            set { base.Cards = value; }
        }


        public void Add(ICollection<ICard> cards)
        {
            foreach (var card in cards)
            {
                this.Add(card);
            }
        }

        public void Add(ICard card)
        {
            Cards.Add(card);
        }

    }
}