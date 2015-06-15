using System.Collections.Generic;

namespace org.gbd.Dominion.Model.Zones
{
    public class Hand : AbstractZone, IHand
    {

        public void Add(ICollection<ICard> cards)
        {
            foreach (var card in cards)
            {
                this.Add(card);
            }
        }

        public void Add(ICard card)
        {
            _cards.Add(card);
        }

    }
}