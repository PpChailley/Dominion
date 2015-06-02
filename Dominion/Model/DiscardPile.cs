using System.Collections.Generic;

namespace org.gbd.Dominion.Model
{
    public class DiscardPile: IDiscardPile
    {
        private IList<ICard> _cards = new List<ICard>();

        public ICollection<ICard> Cards
        {
            get { return _cards; }
        }
    }
}