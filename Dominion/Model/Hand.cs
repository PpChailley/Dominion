using System.Collections.Generic;

namespace org.gbd.Dominion.Model
{
    public class Hand : IHand
    {

        private readonly IList<ICard> _cards = new List<ICard>();

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

        public IList<ICard> Cards
        {
            get { return _cards; }
        }
    }
}