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

        public ILibrary ShuffleToLibrary()
        {
            return new Library(this.Cards);
        }
    }
}
