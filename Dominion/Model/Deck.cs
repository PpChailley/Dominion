using System;
using System.Collections.Generic;
using System.Linq;

namespace org.gbd.Dominion.Model
{
    public class Deck: Queue<ICard>, IDeck
    {
        public ICollection<ICard> Cards = new List<ICard>();

        public ICollection<ICard> Dequeue(int n)
        {
            return Cards.Select(card => this.Dequeue()).ToList();
        }

        public void Shuffle()
        {
            throw new NotImplementedException();
        }

        
    }
}
