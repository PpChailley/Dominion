using System;
using System.Collections.Generic;
using System.Linq;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.Model
{
    public class Deck: IDeck
    {
        private IList<ICard> _cards = new List<ICard>();


        public IList<ICard> Cards
        {
            get { return _cards; }
            set { _cards = value.ToList(); }
        }

        ILibrary IDeck.Shuffle()
        {
            return new Library(this._cards);
        }
    }
}
